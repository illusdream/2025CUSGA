using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

public static class RandomExtension
{
        public static int RandomRange(this (int, int) range)
        {
                return Random.Range(range.Item1, range.Item2);
        }

        public static float RandomRange(this (float, float) range)
        {
                return Random.Range(range.Item1, range.Item2);
        }
        
        public static float RandomRange(this (int, float) range)
        {
                return Random.Range(range.Item1, range.Item2);
        }
        
        public static float RandomRange(this (float, int) range)
        {
                return Random.Range(range.Item1, range.Item2);
        }

        public static T[] Shuffle<T>(this T[] array)
        {
                int n = array.Length;
                var newArray = (T[])array.Clone();
                for (int i = 0; i < n; i++)
                {
                        int k  = Random.Range(0, n - i);
                        (newArray[i], newArray[k]) = (newArray[k], newArray[i]);
                }
                return newArray;
        }

        public static void SelfShuffle<T>(this T[] array)
        {
                int n = array.Length;
                for (int i = 0; i < n; i++)
                {
                        int k  = Random.Range(0, n - i);
                        (array[i], array[k]) = (array[k], array[i]);
                }
        }
        

        public static List<T> Shuffle<T>(this List<T> list)
        {
                int n = list.Count;
                var newlist = (List<T>)list.ConvertAll((res)=>res);
                for (int i = 0; i < n; i++)
                {
                        int k  = Random.Range(0, n - i);
                        (newlist[i], newlist[k]) = (newlist[k], newlist[i]);
                }
                return newlist;
        }

        public static void SelfShuffle<T>(this List<T> list)
        {
                int n = list.Count;
                for (int i = 0; i < n; i++)
                {
                        int k = Random.Range(0, n - i);
                        (list[i], list[k]) = (list[k], list[i]);
                }
        }
        public static T[] ReservoirSampling<T>(this ICollection<T> array,int samplingCount)
        {
                //超出数量直接报错得了
                if (samplingCount > array.Count)
                {
                        throw new ArgumentOutOfRangeException(nameof(samplingCount), samplingCount, $"尝试从{array.Count}中抽取{samplingCount}个结果，你要jb干嘛");
                }
                var res = array.Take(samplingCount).ToArray();
                res.SelfShuffle();
                return res;
        }

        public static T[] NoOverflowReservoirSampling<T>(this ICollection<T> array,int samplingCount)
        {
                //全弄过来
                if (samplingCount > array.Count)
                {
                        samplingCount = array.Count;
                }
                var res = array.Take(samplingCount).ToArray();
                res.SelfShuffle();
                return res;
        }
}