namespace JSLTSharp
{
    public enum SkipJsonNullValues
    {
        /// <summary>
        /// Nothing to do on the output. We keep null values
        /// </summary>
        None = 0,
        /// <summary>
        /// Remove fields with a null value
        /// </summary>
        Object,
        /// <summary>
        /// Remove null values on an array. Keep the array empty if no values
        /// </summary>
        Array,
        /// <summary>
        /// Remove all null values it's a combination of Object and Array
        /// </summary>
        EveryWhere
    }
}
