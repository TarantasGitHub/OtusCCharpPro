namespace ClassLibrary.Exercise1
{
    public static class Class1
    {
        public static T GetMax<T>(this IEnumerable<T> collection, Func<T, float> convertToNumber) where T : class
        {
            if(collection == null || !collection.Any())
            {
                throw new ArgumentException("Коллекция пустая");
            }
            if(convertToNumber == null)
            {
                throw new ArgumentException("Функция преобразования не задана");
            }

            var result = collection.First();            
            float resultValue = convertToNumber(result);

            float itemValue;

            foreach (T item in collection)
            {                
                itemValue = convertToNumber(item);
                if (resultValue < itemValue)
                {
                    result = item;
                    resultValue = itemValue;
                }
            }

            return result;
        }
    }
}