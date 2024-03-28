namespace HellRework
{
    public static class HellUtils
    {
        /// <summary>
        /// A version of Find that returns true if a match is found and outputs the result.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static bool Find<T>(this List<T> l, Predicate<T> predicate, out T result)
        {
            result = default(T);

            if (l.Exists(predicate))
            {
                result = l.Find(predicate);
                return true;
            }

            return false;
        }

        /// <summary>
        /// A version of Find that returns true if a match is found and outputs the result and index.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static bool Find<T>(this List<T> l, Predicate<T> predicate, out T result, out int index)
        {
            index = -1;
            result = default(T);

            if (l.Find(predicate, out result))
            {
                index = l.IndexOf(result);
                return true;
            }

            return false;
        }
    }
}
