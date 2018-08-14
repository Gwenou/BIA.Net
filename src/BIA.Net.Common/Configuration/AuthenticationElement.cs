﻿

namespace BIA.Net.Common.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using static BIA.Net.Common.Configuration.CommonElement;

    public class AuthenticationElement : ConfigurationElement
    {
        [ConfigurationProperty("Parameters", IsRequired = false)]
        public ParametersElement Parameters
        {
            get { return (ParametersElement)this["Parameters"]; }
            set { this["Parameters"] = value; }
        }
        [ConfigurationProperty("Identities", IsRequired = false)]
        public IdentitiesElement Identities
        {
            get { return (IdentitiesElement)this["Identities"]; }
            set { this["Identities"] = value; }
        }

        [ConfigurationProperty("Properties", IsRequired = false)]
        public UserPropertiesElement Properties
        {
            get { return (UserPropertiesElement)this["Properties"]; }
            set { this["Properties"] = value; }
        }
        [ConfigurationProperty("Language", IsRequired = false)]
        public LanguageElement Language
        {
            get { return (LanguageElement)this["Language"]; }
            set { this["Language"] = value; }
        }
        [ConfigurationProperty("Roles", IsRequired = false)]
        public RolesElement Roles
        {
            get { return (RolesElement)this["Roles"]; }
            set { this["Roles"] = value; }
        }
        [ConfigurationProperty("UserProfile", IsRequired = false)]
        public UserProfileElement UserProfile
        {
            get { return (UserProfileElement)this["UserProfile"]; }
            set { this["UserProfile"] = value; }
        }

        public class ParametersElement : ConfigurationElement
        {
            [ConfigurationProperty("Values", IsDefaultCollection = false)]
            [ConfigurationCollection(typeof(KeyValueCollection),
                AddItemName = "add",
                ClearItemsName = "clear",
                RemoveItemName = "remove")]
            public KeyValueCollection Values
            {
                get { return (KeyValueCollection)this["Values"]; }
                set { this["Values"] = value; }
            }

            private List<string> _adDomaines = null;
            public List<string> ADDomains
            {
                get
                {
                    if (_adDomaines == null)
                    {
                        KeyValueElement adDomaines = Values?.GetElemByKey("ADDomains");
                        if (adDomaines != null)
                        {
                            _adDomaines = adDomaines.Value.Split(',').ToList<string>();
                        }
                        else
                        {
                            _adDomaines = new List<string>();
                        }
                    }
                    return _adDomaines;
                }
            }

            private string _caching = null;
            public string Caching
            {
                get
                {
                    if (_caching == null)
                    {
                        KeyValueElement caching = Values?.GetElemByKey("Caching");
                        if (caching != null)
                        {
                            _caching = caching.Value;
                        }
                        else
                        {
                            _caching = "";
                        }
                    }
                    return _caching;
                }
            }
        }
        public class IdentitiesElement : ConfigurationElement
        {
            [ConfigurationProperty("Values", IsDefaultCollection = false)]
            [ConfigurationCollection(typeof(KeyValueCollection),
                AddItemName = "add",
                ClearItemsName = "clear",
                RemoveItemName = "remove")]
            public KeyValueCollection Values
            {
                get { return (KeyValueCollection)this["Values"]; }
                set { this["Values"] = value; }
            }
            [ConfigurationProperty("WindowsIdentities", IsDefaultCollection = false)]
            [ConfigurationCollection(typeof(WindowsIdentityCollection),
                AddItemName = "add",
                ClearItemsName = "clear",
                RemoveItemName = "remove")]
            public WindowsIdentityCollection WindowsIdentities
            {
                get { return (WindowsIdentityCollection)this["WindowsIdentities"]; }
                set { this["WindowsIdentities"] = value; }
            }
            [ConfigurationProperty("Objects", IsDefaultCollection = false)]
            [ConfigurationCollection(typeof(ObjectFieldsCollection),
                AddItemName = "add",
                ClearItemsName = "clear",
                RemoveItemName = "remove")]
            public ObjectFieldsCollection Objects
            {
                get { return (ObjectFieldsCollection)this["Objects"]; }
                set { this["Object"] = value; }
            }

        }
        public class UserPropertiesElement : ConfigurationElement
        {
            [ConfigurationProperty("Values", IsDefaultCollection = false)]
            [ConfigurationCollection(typeof(KeyValueCollection),
                AddItemName = "add",
                ClearItemsName = "clear",
                RemoveItemName = "remove")]
            public KeyValueCollection Values
            {
                get { return (KeyValueCollection)this["Values"]; }
                set { this["Values"] = value; }
            }

            [ConfigurationProperty("AD", IsDefaultCollection = false)]
            [ConfigurationCollection(typeof(ADFieldsCollection),
            AddItemName = "add",
            ClearItemsName = "clear",
            RemoveItemName = "remove")]
            public ADFieldsCollection AD
            {
                get { return (ADFieldsCollection)this["AD"]; }
                set { this["AD"] = value; }
            }

            [ConfigurationProperty("Objects", IsDefaultCollection = false)]
            [ConfigurationCollection(typeof(ObjectFieldsCollection),
            AddItemName = "add",
            ClearItemsName = "clear",
            RemoveItemName = "remove")]
            public ObjectFieldsCollection Objects
            {
                get { return (ObjectFieldsCollection)this["Objects"]; }
                set { this["Object"] = value; }
            }

            [ConfigurationProperty("Functions", IsDefaultCollection = false)]
            [ConfigurationCollection(typeof(FunctionsCollection),
            AddItemName = "add",
            ClearItemsName = "clear",
            RemoveItemName = "remove")]
            public FunctionsCollection Functions
            {
                get { return (FunctionsCollection)this["Functions"]; }
                set { this["Functions"] = value; }
            }

            [ConfigurationProperty("CustomCode", IsRequired = false)]
            public CustomCodeElement CustomCode
            {
                get { return (CustomCodeElement)this["CustomCode"]; }
                set { this["CustomCode"] = value; }
            }

            public class ADFieldsCollection : ConfigCollection<ADFieldsCollection.ADFieldElement>
            {
                public class ADFieldElement : KeyElement
                {

                    [ConfigurationProperty("adfield", IsRequired = false, IsKey = false)]
                    public string Adfield
                    {
                        get
                        {
                            return (string)this["adfield"];
                        }
                        set
                        {
                            this["adfield"] = value;
                        }
                    }
                    [ConfigurationProperty("maxLenght", IsRequired = false, IsKey = false)]
                    public int MaxLenght
                    {
                        get
                        {
                            return (int)this["maxLenght"];
                        }
                        set
                        {
                            this["maxLenght"] = value;
                        }
                    }
                    [ConfigurationProperty("default", IsRequired = false, IsKey = false)]
                    public string Default
                    {
                        get
                        {
                            return (string)this["default"];
                        }
                        set
                        {
                            this["default"] = value;
                        }
                    }
                }

                [ConfigurationProperty("CustomCode", IsRequired = false)]
                public CustomCodeElement CustomCode
                {
                    get { return (CustomCodeElement)this["CustomCode"]; }
                    set { this["CustomCode"] = value; }
                }
            }

            public class FunctionsCollection : ConfigurationElementCollection
            {
                protected override ConfigurationElement CreateNewElement()
                {
                    return new FunctionElement();
                }

                protected override Object GetElementKey(ConfigurationElement element)
                {
                    return ((FunctionElement)element).Key;
                }
                public class FunctionElement : MethodFunctionElement
                {
                    [ConfigurationProperty("key", IsRequired = true, IsKey = true)]
                    public string Key
                    {
                        get
                        {
                            return (string)this["key"];
                        }
                        set
                        {
                            this["key"] = value;
                        }
                    }
                }
            }
        }
        public class RolesElement : ConfigurationElement
        {
            [ConfigurationProperty("Values", IsDefaultCollection = false)]
            [ConfigurationCollection(typeof(KeyCollection),
                AddItemName = "add",
                ClearItemsName = "clear",
                RemoveItemName = "remove")]
            public KeyCollection Values
            {
                get { return (KeyCollection)this["Values"]; }
                set { this["Values"] = value; }
            }
            [ConfigurationProperty("AD", IsDefaultCollection = false)]
            [ConfigurationCollection(typeof(KeyValueCollection),
            AddItemName = "add",
            ClearItemsName = "clear",
            RemoveItemName = "remove")]
            public KeyValueCollection AD
            {
                get { return (KeyValueCollection)this["AD"]; }
                set { this["AD"] = value; }
            }

            [ConfigurationProperty("CustomCode", IsRequired = false)]
            public CustomCodeElement CustomCode
            {
                get { return (CustomCodeElement)this["CustomCode"]; }
                set { this["CustomCode"] = value; }
            }
            
        }
        public class LanguageElement : ConfigurationElement
        {
            [ConfigurationProperty("default", IsRequired = true)]
            public string Default
            {
                get { return (string)this["default"]; }
                set { this["default"] = value; }
            }

            [ConfigurationProperty("Resolvers", IsDefaultCollection = false)]
            [ConfigurationCollection(typeof(ResolversCollection),
                AddItemName = "add",
                ClearItemsName = "clear",
                RemoveItemName = "remove")]
            public ResolversCollection Resolvers
            {
                get { return (ResolversCollection)this["Resolvers"]; }
                set { this["Resolvers"] = value; }
            }

            public class ResolversCollection : ConfigCollection<ResolversCollection.ResolverElement>
            {
                public class ResolverElement : KeyElement
                {
                    [ConfigurationProperty("Mapping", IsDefaultCollection = false)]
                    [ConfigurationCollection(typeof(MappingCollection),
                        AddItemName = "add",
                        ClearItemsName = "clear",
                        RemoveItemName = "remove")]
                    public MappingCollection Mapping
                    {
                        get { return (MappingCollection)this["Mapping"]; }
                        set { this["Mapping"] = value; }
                    }

                    public class MappingCollection : KeyValueCollection
                    {
                        [ConfigurationProperty("property", IsRequired = true)]
                        public string Property
                        {
                            get { return (string)this["property"]; }
                            set { this["property"] = value; }
                        }
                    }
                    [ConfigurationProperty("CustomCode", IsRequired = false)]
                    public CustomCodeElement CustomCode
                    {
                        get { return (CustomCodeElement)this["CustomCode"]; }
                        set { this["CustomCode"] = value; }
                    }
                }
            }
        }
        public class UserProfileElement : ConfigurationElement
        {
            [ConfigurationProperty("Values", IsDefaultCollection = false)]
            [ConfigurationCollection(typeof(KeyValueCollection),
                AddItemName = "add",
                ClearItemsName = "clear",
                RemoveItemName = "remove")]
            public KeyValueCollection Values
            {
                get { return (KeyValueCollection)this["Values"]; }
                set { this["Values"] = value; }
            }

            [ConfigurationProperty("WebServices", IsDefaultCollection = false)]
            [ConfigurationCollection(typeof(WebServicesCollection),
                AddItemName = "add",
                ClearItemsName = "clear",
                RemoveItemName = "remove")]
            public WebServicesCollection WebServices
            {
                get
                {
                    return (WebServicesCollection)this["WebServices"];
                }

                set
                {
                    this["WebServices"] = value;
                }
            }
        }
    }
}