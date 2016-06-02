// Decompiled with JetBrains decompiler
// Type: Tridion.ContentManager.TcmUri
// Assembly: Tridion.Common, Version=7.1.0.52, Culture=neutral, PublicKeyToken=349a39f202fa9b53
// MVID: B52A7802-8368-4BB0-9B15-67C30FD0EA31
// Assembly location: C:\Workspace\Schneider\webmachine-helpers\betc-migration\lib\Tridion.Common.dll

using System;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Tridion.ContentManager.CoreService.Client;

namespace chrismrgn.sdl.tridion.core
{
    public class TcmUri
    {
        private static readonly Regex _tcmUriRegEx = new Regex("^tcm:(?<pubId>[0-9]+)-(?<itemId>[0-9]+)(-(?<itemType>[0-9]+))?(-v(?<version>[0-9]+))?$", RegexOptions.Compiled);
        private static readonly TcmUri _uriNull = new TcmUri();

        public static readonly TcmUri SystemRepositoryUri = new TcmUri(0, ItemType.Publication | ItemType.Folder | ItemType.StructureGroup);
        private const int _editableVersion = 0;
        internal const ItemType SystemRepositoryItemType = ItemType.Publication | ItemType.Folder | ItemType.StructureGroup;
        private uint _publicationId;
        private uint _itemId;
        private ItemType _itemType;
        private uint? _version;

        public static TcmUri UriNull
        {
            get
            {
                return TcmUri._uriNull;
            }
        }

        public int ItemId
        {
            get
            {
                if (this._itemType == ItemType.ApprovalStatus || (int)this._itemId != 0)
                    return (int)this._itemId;
                return -1;
            }
        }

        public ItemType ItemType
        {
            get
            {
                return this._itemType;
            }
        }

        public int PublicationId
        {
            get
            {
                if ((int)this._publicationId != 0)
                    return (int)this._publicationId;
                return -1;
            }
        }

        public int Version
        {
            get
            {
                if (this._version.HasValue)
                    return (int)this._version.Value;
                return -1;
            }
        }

        public int ContextRepositoryId
        {
            get
            {
                return this.GetContextRepositoryId(true);
            }
        }

        public bool IsEditableVersion
        {
            get
            {
                if ((int)this._itemId == 0 && this._itemType == ItemType.None && (int)this._publicationId == 0)
                    return true;
                uint? nullable = this._version;
                if ((int)nullable.GetValueOrDefault() == 0)
                    return nullable.HasValue;
                return false;
            }
        }

        public bool IsSystemWide
        {
            get
            {
                return (this._itemType & (ItemType)196608) == (ItemType)65536 || (this._itemType == ItemType.ApprovalStatus || this._itemType == ItemType.Publication);
            }
        }

        public bool IsRepositoryLocal
        {
            get
            {
                return (this._itemType & (ItemType)196608) != ItemType.None ? this._itemType == ItemType.ProcessDefinition : this._itemType != ItemType.Publication;
            }
        }

        public bool IsVersionless
        {
            get
            {
                return !this._version.HasValue;
            }
        }

        public bool IsSystemRepository
        {
            get
            {
                if ((int)this._itemId == 0 && (int)this._publicationId == 0)
                    return (this._itemType & TcmUri.SystemRepositoryUri.ItemType) == this._itemType;
                return false;
            }
        }

        public bool IsUriNull
        {
            get
            {
                if ((int)this._itemId == 0 && this._itemType == ItemType.None)
                    return (int)this._publicationId == 0;
                return false;
            }
        }

        private TcmUri()
        {
        }

        public TcmUri(int itemId, ItemType itemType)
        {
            if (itemId < 0)
                throw new ArgumentOutOfRangeException("itemId");
            this.AssertValidItemType(itemType);
            this._itemId = (uint)itemId;
            this._itemType = itemType;
        }

        public TcmUri(int itemId, ItemType itemType, int publicationId)
        {
            if (itemId < 0)
                throw new ArgumentOutOfRangeException("itemId");
            if (publicationId < 0 && publicationId != -1)
                throw new ArgumentOutOfRangeException("publicationId");
            this.AssertValidItemType(itemType);
            this._itemId = (uint)itemId;
            this._itemType = itemType;
            this._publicationId = publicationId == -1 ? 0U : (uint)publicationId;
        }

        public TcmUri(int itemId, ItemType itemType, int publicationId, int version)
        {
            if (itemId < 0)
                throw new ArgumentOutOfRangeException("itemId");
            if (publicationId < 0 && publicationId != -1)
                throw new ArgumentOutOfRangeException("publicationId");
            this.AssertValidItemType(itemType);
            this._itemId = (uint)itemId;
            this._itemType = itemType;
            this._publicationId = publicationId == -1 ? 0U : (uint)publicationId;
            this._version = version < 0 ? new uint?() : new uint?((uint)version);
        }

        public TcmUri(string uri)
        {
            this.Parse(uri);
        }

       public static implicit operator string(TcmUri source)
        {
            if (source != (TcmUri)null)
                return source.ToString();
            return (string)null;
        }

        public static bool operator ==(TcmUri objA, TcmUri objB)
        {
            if (TcmUri.IsNull((object)objA) || TcmUri.IsNull((object)objB))
            {
                if (TcmUri.IsNull((object)objA))
                    return TcmUri.IsNull((object)objB);
                return false;
            }
            if (objA.ItemType == objB.ItemType && objA.ItemId == objB.ItemId && objA.PublicationId == objB.PublicationId)
                return objA.Version == objB.Version;
            return false;
        }

        public static bool operator !=(TcmUri objA, TcmUri objB)
        {
            if (TcmUri.IsNull((object)objA) || TcmUri.IsNull((object)objB))
            {
                if (TcmUri.IsNull((object)objA))
                    return !TcmUri.IsNull((object)objB);
                return true;
            }
            if (objA.ItemType == objB.ItemType && objA.ItemId == objB.ItemId && objA.PublicationId == objB.PublicationId)
                return objA.Version != objB.Version;
            return true;
        }

        public static bool operator ==(TcmUri objA, string objB)
        {
            if (!TcmUri.IsNull((object)objA) && !TcmUri.IsNull((object)objB))
                return objA == new TcmUri(objB);
            if (TcmUri.IsNull((object)objA))
                return TcmUri.IsNull((object)objB);
            return false;
        }

        public static bool operator !=(TcmUri objA, string objB)
        {
            if (!TcmUri.IsNull((object)objA) && !TcmUri.IsNull((object)objB))
                return objA != new TcmUri(objB);
            if (TcmUri.IsNull((object)objA))
                return !TcmUri.IsNull((object)objB);
            return true;
        }

        public int GetContextRepositoryId(bool expectSystemRepository)
        {
            if (this._itemType == ItemType.Publication)
                return (int)this._itemId;
            if ((int)this._publicationId != 0)
                return (int)this._publicationId;
            return !expectSystemRepository ? -1 : 0;
        }

        public TcmUri GetContextRepositoryUri()
        {
            if (this._itemType == ItemType.Publication)
                return this;
            if ((int)this._publicationId != 0)
                return new TcmUri((int)this._publicationId, ItemType.Publication);
            return TcmUri.SystemRepositoryUri;
        }

        public TcmUri GetVersionlessUri()
        {
            return new TcmUri((int)this._itemId, this._itemType, (int)this._publicationId);
        }

        public static bool IsNullOrUriNull(TcmUri id)
        {
            if (!(id == (TcmUri)null))
                return id.IsUriNull;
            return true;
        }

        public static bool IsValid(string uri)
        {
            if (uri != null)
                return TcmUri._tcmUriRegEx.IsMatch(uri);
            return false;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("tcm");
            stringBuilder.Append(":");
            stringBuilder.Append(this._publicationId);
            stringBuilder.Append("-");
            stringBuilder.Append(this._itemId);
            if (this._itemType != ItemType.Component)
            {
                stringBuilder.Append("-");
                stringBuilder.Append((int)this._itemType);
            }
            if (this._version.HasValue)
            {
                stringBuilder.Append("-");
                stringBuilder.Append("v");
                stringBuilder.Append((object)this._version);
            }
            return stringBuilder.ToString();
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is TcmUri)
                return this == (TcmUri)obj;
            return false;
        }

        public new static bool Equals(object objA, object objB)
        {
            if (objA != null)
                return objA.Equals(objB);
            return objB == null;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        internal static XmlQualifiedName ProvideSchema(XmlSchemaSet xsdSet)
        {
            XmlSchema schema = (XmlSchema)null;
            ICollection collection = xsdSet.Schemas("http://www.sdltridion.com/ContentManager/R6");
            if (collection.Count > 0)
            {
                IEnumerator enumerator = collection.GetEnumerator();
                try
                {
                    if (enumerator.MoveNext())
                        schema = (XmlSchema)enumerator.Current;
                }
                finally
                {
                    IDisposable disposable = enumerator as IDisposable;
                    if (disposable != null)
                        disposable.Dispose();
                }
            }
            else
            {
                schema = new XmlSchema();
                schema.TargetNamespace = "http://www.sdltridion.com/ContentManager/R6";
                xsdSet.Add(schema);
            }
            XmlSchemaSimpleType schemaSimpleType1 = new XmlSchemaSimpleType();
            schemaSimpleType1.Name = "TcmUri";
            XmlSchemaSimpleType schemaSimpleType2 = schemaSimpleType1;
            schemaSimpleType2.Content = (XmlSchemaSimpleTypeContent)new XmlSchemaSimpleTypeRestriction()
            {
                BaseTypeName = new XmlQualifiedName("string", "http://www.w3.org/2001/XMLSchema")
            };
            schema.Items.Add((XmlSchemaObject)schemaSimpleType2);
            return new XmlQualifiedName("TcmUri", "http://www.sdltridion.com/ContentManager/R6");
        }

        private void AssertValidItemType(ItemType itemType)
        {
            if (itemType == ItemType.UnknownByClient)
                throw new Exception("UnknownByClientNotAllowed ItemType");
        }

        private static bool IsNull(object obj)
        {
            return obj == null;
        }

        private void Parse(string uri)
        {
            try
            {
                Match match = TcmUri._tcmUriRegEx.Match(uri);
                if (!match.Success)
                    throw new ArgumentException(uri);
                this._publicationId = (uint)int.Parse(match.Groups["pubId"].Value);
                this._itemId = (uint)int.Parse(match.Groups["itemId"].Value);
                this._itemType = match.Groups["itemType"].Captures.Count <= 0 ? ItemType.Component : (ItemType)int.Parse(match.Groups["itemType"].Value);
                this.AssertValidItemType(this._itemType);
                if (match.Groups["version"].Captures.Count <= 0)
                    return;
                this._version = new uint?((uint)int.Parse(match.Groups["version"].Value));
            }
            catch (Exception ex)
            {
                throw new ArgumentException(uri, ex);
            }
        }
    }
}
