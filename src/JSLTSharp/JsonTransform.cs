using JSLTSharp.JsonTransforms;
using JSLTSharp.JsonTransforms.Abstractions;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace JSLTSharp
{
    public class JsonTransform
    {
        /// <summary>
        /// Define how values must be cleaned are transformation
        /// Default : <see cref="SkipJsonNullValues.None"/>
        /// </summary>
        public SkipJsonNullValues SkipJsonNull { get; set; } = SkipJsonNullValues.None;

        private readonly Dictionary<string, IJsonTransformConditionalCustomOperation> conditionalOperations;
        private readonly Dictionary<string, IJsonTransformCustomOperation> customOperations;

        public JsonTransform()
            : this(new List<IJsonTransformConditionalCustomOperation>(),
                    new List<IJsonTransformCustomOperation>())
        {
        }

        /// <summary>
        /// Create the transformation engine with custom functions
        /// </summary>
        /// <param name="conditionalOperations">List of custom function that returns a true/false result. Can be used for if/else if for ex.</param>
        /// <param name="customOperations">List of custom functions that apply a transformation on a json content. Convert to integer, ...</param>
        public JsonTransform(IEnumerable<IJsonTransformConditionalCustomOperation> conditionalOperations,
                                IEnumerable<IJsonTransformCustomOperation> customOperations)
        {
            this.conditionalOperations = conditionalOperations.ToDictionary(e => e.OperationName, e => e, StringComparer.InvariantCultureIgnoreCase);
            this.customOperations = customOperations.ToDictionary(e => e.OperationName, e => e, StringComparer.InvariantCultureIgnoreCase);
        }

        public string Transform(string jsonContent, string jsonTransformationDescription)
        {
            // Read, transform to JToken, serialize output :
            var jtokenContent = JToken.Parse(jsonContent);

            var result = Transform(jtokenContent, jsonTransformationDescription);

            return result.ToString();
        }

        public JToken Transform(JToken jsonContent, string jsonTransformationDescription)
        {
            if (string.IsNullOrWhiteSpace(jsonTransformationDescription))
                throw new ArgumentNullException(nameof(jsonTransformationDescription), "No transformation defined");

            // TODO : Add support of functions :
            var result = JToken.Parse(jsonTransformationDescription);

            if (result is JObject)
                ApplyTransformation(result, jsonContent);
            else if (result is JArray)
                ApplyTransformation(((JArray)result)[0], jsonContent);

            return result;
        }

        /// <summary>
        /// Apply transformation on an object
        /// </summary>
        /// <param name="transformation"></param>
        /// <param name="dataSource"></param>
        private void ApplyTransformation(JToken transformation, JToken dataSource)
        {
            if (transformation is null)
                throw new ArgumentNullException(nameof(transformation));
            if (dataSource is null)
                throw new ArgumentNullException(nameof(dataSource));

            switch (transformation.Type)
            {
                case JTokenType.Object:
                    var currentObject = (JObject)transformation;
                    var children = currentObject.Properties().ToList();
                    foreach (var child in children)
                    {
                        ApplyTransformation(child, dataSource);
                    }
                    break;
                case JTokenType.Property:
                    var currentProp = transformation.Value<JProperty>();
                    if (currentProp.Name.StartsWith("->if(", StringComparison.InvariantCultureIgnoreCase))
                    {
                        // if methods. All booleans must be true to returns content :
                        var functions = currentProp.Name[5..^1];
                        var splitFunctions = functions.Split("->", StringSplitOptions.RemoveEmptyEntries);
                        bool functionsSuccess = true;
                        for (int i = 0; i < splitFunctions.Length && functionsSuccess; i++)
                        {
                            var functionI = splitFunctions[i].IndexOf('(');
                            if (functionI == -1 || !splitFunctions[i].EndsWith(')'))
                                throw new InvalidOperationException($"Function {splitFunctions[i]} is not correctly formatted");

                            var functionName = splitFunctions[i][..functionI];

                            if (conditionalOperations.TryGetValue(functionName, out var customOperation))
                            {
                                var functionParameters = splitFunctions[i].Substring(functionI + 1, splitFunctions[i].Length - functionI - 2);
                                functionsSuccess &= customOperation.Apply(dataSource, currentProp.Value, functionParameters.Split(','));
                            }
                            else
                                functionsSuccess = false;
                        }
                        var currentObject1 = currentProp.Value as JObject;
                        if (functionsSuccess && currentObject1 != null)
                        {
                            var childrenProperties = currentObject1.Properties().Where(e => !e.Name.StartsWith("->elseif(")).ToList();
                            foreach (var child in childrenProperties)
                            {
                                ApplyTransformation(child, dataSource);
                            }
                            currentProp.AddBeforeSelf(currentObject1.Properties().Where(e => !e.Name.StartsWith("->elseif(")));
                        }
                        else if (currentObject1 != null)
                        {
                            // Function failed, we will check else/elseif operations :
                            var elseIfProperties = currentObject1.Properties().Where(e => e.Name.StartsWith("->elseif(")).ToList();
                            functionsSuccess = false;
                            // We check all (->elseif) operations while functionsSuccess is false
                            for (int pi = 0; pi < elseIfProperties.Count && !functionsSuccess; pi++)
                            {
                                // We check this if condition :
                                currentProp = elseIfProperties[pi];
                                functions = currentProp.Name[9..^1];
                                if (string.IsNullOrWhiteSpace(functions))
                                    functionsSuccess = true;
                                else
                                {
                                    splitFunctions = functions.Split("->", StringSplitOptions.RemoveEmptyEntries);
                                    var subFunctionsSuccess = true;
                                    for (int i = 0; i < splitFunctions.Length && subFunctionsSuccess; i++)
                                    {
                                        var functionI = splitFunctions[i].IndexOf('(');
                                        if (functionI == -1 || !splitFunctions[i].EndsWith(')'))
                                            throw new InvalidOperationException($"Function {splitFunctions[i]} is not correctly formatted");

                                        var functionName = splitFunctions[i][..functionI];
                                        if (conditionalOperations.TryGetValue(functionName, out var customOperation))
                                        {
                                            var functionParameters = splitFunctions[i].Substring(functionI + 1, splitFunctions[i].Length - functionI - 2);
                                            subFunctionsSuccess &= customOperation.Apply(dataSource, currentProp.Value, functionParameters.Split(','));
                                        }
                                        else
                                            subFunctionsSuccess = false;
                                    }

                                    functionsSuccess = subFunctionsSuccess;
                                }
                                if (functionsSuccess)
                                {
                                    currentObject1 = currentProp.Value as JObject;
                                    var childrenProperties = currentObject1.Properties().Where(e => !e.Name.StartsWith("->elseif(")).ToList();
                                    foreach (var child in childrenProperties)
                                    {
                                        ApplyTransformation(child, dataSource);
                                    }
                                    currentProp.Parent.Parent.AddBeforeSelf(currentObject1.Properties().Where(e => !e.Name.StartsWith("->elseif(")).ToList());
                                }
                            }

                            // Now we clean elseif properties :
                            for (int pi = 0; pi < elseIfProperties.Count; pi++)
                            {
                                elseIfProperties[pi].Remove();
                            }
                        }

                        // we reset the original property also
                        currentProp = transformation.Value<JProperty>();
                        currentProp.Remove();
                    }
                    else if (currentProp.Name.Contains("->loop(", StringComparison.InvariantCultureIgnoreCase))
                    {
                        // Array loop !
                        var split = currentProp.Name.Split("->", StringSplitOptions.RemoveEmptyEntries);
                        var fieldName = split[0];

                        var functionI = split[1].IndexOf('(');
                        if (functionI == -1 || !split[1].EndsWith(')'))
                            throw new InvalidOperationException($"Loop function {split[1]} is not correctly formatted");

                        // Array elements :
                        JToken arrayToken = null;
                        if (fieldName.EndsWith("]"))
                        {
                            // We have some filters, so we must use SelectTokens to get the array of fields :
                            arrayToken = dataSource.SelectTokens(fieldName) as JArray;
                        }
                        else
                        {
                            // Select token. It must returns a JArray :
                            arrayToken = dataSource.SelectToken(fieldName) as JArray;
                        }

                        var parentField = currentProp?.Parent?.Parent as JProperty;

                        if (arrayToken != null)
                        {
                            for (int i = 1; i < split.Length; i++)
                            {
                                var function = split[i];

                                var functionJ = split[i].IndexOf('(');
                                if (functionJ == -1 || !split[i].EndsWith(')'))
                                    throw new InvalidOperationException($"Loop function {split[i]} is not correctly formatted");

                                var functionName = split[i][..functionJ];
                                var functionParameters = split[i].Substring(functionJ + 1, split[i].Length - functionJ - 2);
                                List<string> parameterValues = functionParameters.Split(',').ToList();

                                if (functionName.Equals("loop"))
                                {
                                    var model = currentProp.Value<JProperty>().Value;

                                    ConcurrentBag<JToken> models = new();

                                    arrayToken.AsParallel().ForAll(item =>
                                    {
                                        models.Add(model.DeepClone());
                                    });

                                    JArray resultArray = new();
                                    foreach (var item in arrayToken)
                                    {
                                        models.TryTake(out JToken entry);

                                        // Insert current entry for context :
                                        (dataSource as JObject).Add(new JProperty(parameterValues[0], item));

                                        // Apply transformation :
                                        if (entry.Type == JTokenType.String)
                                        {
                                            JToken tokenEntry = ApplyTransformationOnEntry(dataSource, null, entry as JValue);
                                            resultArray.Add(tokenEntry);
                                        }
                                        else
                                        {
                                            ApplyTransformation(entry, dataSource);
                                            resultArray.Add(entry);
                                        }

                                        // Remove context entry :
                                        (dataSource as JObject).Remove(parameterValues[0]);
                                    }
                                    arrayToken = resultArray;
                                }
                                else
                                {
                                    if (customOperations.TryGetValue(functionName, out var customOperation))
                                    {
                                        arrayToken = customOperation.Apply(dataSource, arrayToken, parameterValues);
                                    }
                                    else
                                    {
                                        // Custom functions on the array :
                                        if (functionName.Equals("NullIfEmpty", StringComparison.InvariantCultureIgnoreCase)
                                             && !arrayToken.Any())
                                        {
                                            if (parentField != null)
                                                parentField.Value.Replace(JValue.CreateNull());
                                            else if (currentProp?.Parent?.Parent is JArray && arrayToken is JArray)
                                            {
                                                var parentArray = (JArray)currentProp.Parent.Parent;
                                                parentArray.Clear();
                                            }
                                        }
                                        else if (functionName.Equals("RemoveIfEmpty", StringComparison.InvariantCultureIgnoreCase)
                                             && !arrayToken.Any())
                                        {
                                            if (parentField != null)
                                                parentField.Remove();
                                            else if (currentProp?.Parent?.Parent is JArray && arrayToken is JArray)
                                            {
                                                var parentArray = (JArray)currentProp.Parent.Parent;
                                                parentArray.Clear();
                                            }
                                        }
                                        else if (functionName.Equals("RemoveIfNull", StringComparison.InvariantCultureIgnoreCase)
                                            && arrayToken.Type == JTokenType.Null)
                                        {
                                            if (parentField != null)
                                                parentField.Remove();
                                            else if (currentProp?.Parent?.Parent is JArray && arrayToken is JArray)
                                            {
                                                var parentArray = (JArray)currentProp.Parent.Parent;
                                                parentArray.Clear();
                                            }
                                        }
                                        else if (!arrayToken.Any())
                                            throw new InvalidOperationException($"Function {function} is unknowed");
                                    }
                                }
                            }
                        }

                        if (parentField != null)
                            parentField.Value.Replace(arrayToken);
                        else if (currentProp?.Parent?.Parent is JArray)
                        {
                            var parentArray = (JArray)currentProp.Parent.Parent;
                            currentProp.Parent.Remove();
                            if (arrayToken is JArray)
                            {
                                foreach (var item in (JArray)arrayToken)
                                    parentArray.Add(item);
                            }
                        }
                    }
                    else if (currentProp.Name.Contains("->", StringComparison.InvariantCultureIgnoreCase))
                    {
                        // Conditional operation
                        var split = currentProp.Name.Split("->", StringSplitOptions.RemoveEmptyEntries);
                        var fieldName = split[0];
                        bool successOperation = true;
                        for (int i = 1; i < split.Length && successOperation; i++)
                        {
                            var functionI = split[i].IndexOf('(');
                            if (functionI == -1 || !split[i].EndsWith(')'))
                                throw new InvalidOperationException($"Loop function {split[i]} is not correctly formatted");

                            var functionName = split[i][..functionI];

                            if (conditionalOperations.TryGetValue(functionName, out var conditionalOperation))
                            {
                                var functionParameters = split[i].Substring(functionI + 1, split[i].Length - functionI - 2);
                                successOperation = conditionalOperation.Apply(dataSource, currentProp.Value, functionParameters.Split(','));
                            }
                            else
                                successOperation = false;
                        }
                        if (successOperation)
                        {
                            ApplyTransformation(currentProp.Value, dataSource);
                            currentProp.Replace(new JProperty(fieldName, currentProp.Value));
                        }
                        else
                            currentProp.Remove();
                    }
                    else
                        ApplyTransformation(currentProp.Value, dataSource);
                    break;
                case JTokenType.Array:
                    var array = transformation.Value<JArray>();
                    var arrayChildren = array.Children().ToList();
                    arrayChildren.AsParallel().AsOrdered().ForAll(item =>
                    {
                        ApplyTransformation(item, dataSource);
                    });

                    break;
                default:
                    ApplyTransformationOnEntry(dataSource, transformation.Parent, transformation as JValue);
                    break;
            }
        }


        private JToken ApplyTransformationOnEntry(JToken dataSource, JToken currentProp, JValue value)
        {
            // Apply transformation from the value :
            var stringValue = value.Value as string;
            if (string.IsNullOrWhiteSpace(stringValue))
                return JValue.CreateNull(); // Nothing to do

            var split = stringValue.Split("->");
            var fieldName = split[0];

            JToken token;
            if (string.IsNullOrWhiteSpace(fieldName))
                token = JValue.CreateNull();// We simulate a null token
            else if (fieldName.StartsWith("$.") || fieldName.StartsWith("["))
                token = dataSource.SelectToken(fieldName);
            else
                token = JValue.FromObject(fieldName);

            if (token is null)
                token = JValue.CreateNull();

            var currentToken = token;

            // Probably a function :
            for (int i = 1; i < split.Length && currentProp?.Parent != null; i++)
            {
                var function = split[i];

                var functionI = split[i].IndexOf('(');
                if (functionI == -1 || !split[1].EndsWith(')'))
                    throw new InvalidOperationException($"Loop function {split[i]} is not correctly formatted");

                var functionName = split[i][..functionI];
                var functionParameters = split[i].Substring(functionI + 1, split[i].Length - functionI - 2);

                var parameterValues = functionParameters.Split(',');

                if (customOperations.TryGetValue(functionName, out var customOperation))
                {
                    currentToken = customOperation.Apply(dataSource, currentToken, parameterValues);
                }
                else
                {
                    switch (functionName.ToLower())
                    {
                        case "defaultstring":
                            if (currentToken.Type == JTokenType.Null ||
                                (currentToken.Type == JTokenType.String && string.IsNullOrEmpty(currentToken.Value<string>())))
                                currentToken = JValue.FromObject(string.Empty);
                            break;
                        case "ifnotnullandempty":
                            bool toRemove = false;

                            if (currentToken.Type == JTokenType.Null ||
                                (currentToken.Type == JTokenType.String && string.IsNullOrEmpty(currentToken.Value<string>())))
                                toRemove = true;

                            if (toRemove)
                            {
                                currentToken = JValue.CreateNull();
                                currentProp.Remove();
                            }
                            break;
                        case "ifexists":
                            if (parameterValues.Length != 1)
                                throw new InvalidOperationException($"You must provide only one parameter for function {function}");

                            var selectTokenForIf = dataSource.SelectTokens(parameterValues[0]);
                            if (selectTokenForIf is null || !selectTokenForIf.Any())
                            {
                                currentToken = JValue.CreateNull();
                                currentProp.Remove();
                            }
                            break;
                        default:
                            // Nothing to do, the function is not reconized
                            break;
                    }
                }
            }

            if (currentProp?.Parent != null)
            {
                value.Replace(currentToken);
            }

            return currentToken;
        }
    }
}