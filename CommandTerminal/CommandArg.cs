using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CommandTerminalPlus
{
    public struct CommandArg
    {
        public string String { get; set; }

        public int Int {
            get {
                int int_value;

                if (int.TryParse(String, out int_value)) {
                    return int_value;
                }

                TypeError("int");
                return 0;
            }
        }

        public float Float {
            get {
                float float_value;

                if (float.TryParse(String, out float_value)) {
                    return float_value;
                }

                TypeError("float");
                return 0;
            }
        }

        public bool Bool {
            get {
                if (string.Compare(String, "TRUE", ignoreCase: true) == 0) {
                    return true;
                }

                if (string.Compare(String, "FALSE", ignoreCase: true) == 0) {
                    return false;
                }

                TypeError("bool");
                return false;
            }
        }

        public T AsEnum<T>() where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
                throw new Exception($"type {typeof(T).FullName} is not an enum - you can't read the CommandArg this way");

            if(Enum.TryParse(String, ignoreCase: true, out T value))
            {
                return value;
            }
            else
            {
                TypeError(typeof(T).FullName);
                    throw new Exception($"value {String} not found in enumerated type {typeof(T).FullName}");
                return default;
            }
        }

        public override string ToString() {
            return String;
        }

        void TypeError(string expected_type) {
            Terminal.Shell.IssueErrorMessage(
                "Incorrect type for {0}, expected <{1}>",
                String, expected_type
            );
        }
    }
}
