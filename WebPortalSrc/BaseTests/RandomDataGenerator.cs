using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseTests {
    class RandomDataGenerator {
        private readonly Random _random = new Random();
        private const int MIN_RANDOM_INT = 10000;
        private const int MAX_RANDOM_INT = 1000000;

        public int RandomInt(){
            return _random.Next(MIN_RANDOM_INT, MAX_RANDOM_INT);
        }

        public string RandomString(int size){
            var sb = new StringBuilder();

            for (var i = 0; i < size; ++i){
                sb.Append((char)_random.Next(65, 65 + 26));
            }
            return sb.ToString();
        }

        public int RandomIntBetween(int start, int end){
            return _random.Next(start, end);
        }

        public bool RandomBool(){
            int randomInt = _random.Next(MIN_RANDOM_INT, MAX_RANDOM_INT);
            return randomInt > (MIN_RANDOM_INT + ((MAX_RANDOM_INT - MIN_RANDOM_INT) / 2));
        }
    }
}
