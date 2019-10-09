using NJsonSchema.Generation;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace NJsonSchema.Tests.Generation
{
    public class DictionaryTests
    {
        public enum PropertyName
        {
            Name,
            Gender
        }

        public class EnumKeyDictionaryTest
        {
            public Dictionary<PropertyName, string> Mapping { get; set; }

            public IDictionary<PropertyName, string> Mapping2 { get; set; }

            public IDictionary<PropertyName, int?> Mapping3 { get; set; }

            public TestDictionary Mapping4 { get; set; }

            public TestReadonlyDictionary Mapping5 { get; set; }
        }


        [Fact]
        public async Task When_dictionary_key_is_enum_then_csharp_has_enum_key()
        {
            //// Act
            var schema = JsonSchema.FromType<EnumKeyDictionaryTest>();
            var data = schema.ToJson();

            //// Assert
            Assert.True(schema.Properties["Mapping"].IsDictionary);
            Assert.True(schema.Properties["Mapping"].DictionaryKey.ActualSchema.IsEnumeration);

            Assert.True(schema.Properties["Mapping2"].IsDictionary);
            Assert.True(schema.Properties["Mapping2"].DictionaryKey.ActualSchema.IsEnumeration);

            Assert.False(schema.Properties["Mapping2"].DictionaryKey.IsNullable(SchemaType.JsonSchema));
            Assert.False(schema.Properties["Mapping2"].AdditionalPropertiesSchema.IsNullable(SchemaType.JsonSchema));
        }

        [Fact]
        public async Task When_value_type_is_nullable_then_json_schema_is_nullable()
        {
            //// Act
            var schema = JsonSchema.FromType<EnumKeyDictionaryTest>();
            var data = schema.ToJson();

            //// Assert
            Assert.True(schema.Properties["Mapping3"].IsDictionary);
            Assert.True(schema.Properties["Mapping3"].AdditionalPropertiesSchema.IsNullable(SchemaType.JsonSchema));
        }

        [Fact]
        public async Task When_value_type_is_nullable_then_json_schema_is_nullable_Swagger2()
        {
            //// Act
            var schema = JsonSchema.FromType<EnumKeyDictionaryTest>(new JsonSchemaGeneratorSettings
            {
                SchemaType = SchemaType.Swagger2,
                GenerateCustomNullableProperties = true
            });
            var data = schema.ToJson();

            //// Assert
            Assert.True(schema.Properties["Mapping3"].IsDictionary);
            Assert.True(schema.Properties["Mapping3"].AdditionalPropertiesSchema.IsNullable(SchemaType.Swagger2));
        }

        [Fact]
        public async Task When_dictionary_only_implements_generic_idictionary_then_is_still_dictionary()
        {
            //// Act
            var schema = JsonSchema.FromType<EnumKeyDictionaryTest>();

            //// Assert
            Assert.True(schema.Properties["Mapping4"].IsDictionary);
        }

        [Fact]
        public async Task When_dictionary_only_implements_generic_ireadonlydictionary_then_is_still_dictionary()
        {
            //// Act
            var schema = JsonSchema.FromType<EnumKeyDictionaryTest>();

            //// Assert
            Assert.True(schema.Properties["Mapping5"].IsDictionary);
        }

        public class TestDictionary : IDictionary<string, object>
        {
            public object this[string key] { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

            public ICollection<string> Keys => throw new System.NotImplementedException();

            public ICollection<object> Values => throw new System.NotImplementedException();

            public int Count => throw new System.NotImplementedException();

            public bool IsReadOnly => throw new System.NotImplementedException();

            public void Add(string key, object value)
            {
                throw new System.NotImplementedException();
            }

            public void Add(KeyValuePair<string, object> item)
            {
                throw new System.NotImplementedException();
            }

            public void Clear()
            {
                throw new System.NotImplementedException();
            }

            public bool Contains(KeyValuePair<string, object> item)
            {
                throw new System.NotImplementedException();
            }

            public bool ContainsKey(string key)
            {
                throw new System.NotImplementedException();
            }

            public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
            {
                throw new System.NotImplementedException();
            }

            public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
            {
                throw new System.NotImplementedException();
            }

            public bool Remove(string key)
            {
                throw new System.NotImplementedException();
            }

            public bool Remove(KeyValuePair<string, object> item)
            {
                throw new System.NotImplementedException();
            }

            public bool TryGetValue(string key, out object value)
            {
                throw new System.NotImplementedException();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                throw new System.NotImplementedException();
            }
        }

        public class TestReadonlyDictionary : IReadOnlyDictionary<string, object>
        {
            public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
            {
                throw new System.NotImplementedException();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }

            public int Count { get; }
            public bool ContainsKey(string key)
            {
                throw new System.NotImplementedException();
            }

            public bool TryGetValue(string key, out object value)
            {
                throw new System.NotImplementedException();
            }

            public object this[string key] => throw new System.NotImplementedException();

            public IEnumerable<string> Keys { get; }
            public IEnumerable<object> Values { get; }
        }
    }
}