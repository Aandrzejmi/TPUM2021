/* ========================================================================
 * Copyright (c) 2005-2016 The OPC Foundation, Inc. All rights reserved.
 *
 * OPC Foundation MIT License 1.00
 *
 * Permission is hereby granted, free of charge, to any person
 * obtaining a copy of this software and associated documentation
 * files (the "Software"), to deal in the Software without
 * restriction, including without limitation the rights to use,
 * copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the
 * Software is furnished to do so, subject to the following
 * conditions:
 *
 * The above copyright notice and this permission notice shall be
 * included in all copies or substantial portions of the Software.
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
 * EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
 * OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
 * NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
 * HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
 * WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
 * FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
 * OTHER DEALINGS IN THE SOFTWARE.
 *
 * The complete license agreement can be found here:
 * http://opcfoundation.org/License/MIT/1.00/
 * ======================================================================*/

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Runtime.Serialization;
using Opc.Ua;

namespace TPUM.OPC
{
    #region Object Identifiers
    /// <summary>
    /// A class that declares constants for all Objects in the Model Design.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public static partial class Objects
    {
        /// <summary>
        /// The identifier for the CEvidenceEntry_Product Object.
        /// </summary>
        public const uint CEvidenceEntry_Product = 16;

        /// <summary>
        /// The identifier for the COrder_Client Object.
        /// </summary>
        public const uint COrder_Client = 27;

        /// <summary>
        /// The identifier for the COrder_Entries Object.
        /// </summary>
        public const uint COrder_Entries = 31;

        /// <summary>
        /// The identifier for the COrder_Entries_Product Object.
        /// </summary>
        public const uint COrder_Entries_Product = 33;
    }
    #endregion

    #region ObjectType Identifiers
    /// <summary>
    /// A class that declares constants for all ObjectTypes in the Model Design.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public static partial class ObjectTypes
    {
        /// <summary>
        /// The identifier for the CClient ObjectType.
        /// </summary>
        public const uint CClient = 11;

        /// <summary>
        /// The identifier for the CEvidenceEntry ObjectType.
        /// </summary>
        public const uint CEvidenceEntry = 15;

        /// <summary>
        /// The identifier for the CProduct ObjectType.
        /// </summary>
        public const uint CProduct = 18;

        /// <summary>
        /// The identifier for the COrder ObjectType.
        /// </summary>
        public const uint COrder = 22;

        /// <summary>
        /// The identifier for the CSendRequest ObjectType.
        /// </summary>
        public const uint CSendRequest = 37;

        /// <summary>
        /// The identifier for the CSubscribeUpdates ObjectType.
        /// </summary>
        public const uint CSubscribeUpdates = 40;
    }
    #endregion

    #region Variable Identifiers
    /// <summary>
    /// A class that declares constants for all Variables in the Model Design.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public static partial class Variables
    {
        /// <summary>
        /// The identifier for the CClient_ID Variable.
        /// </summary>
        public const uint CClient_ID = 12;

        /// <summary>
        /// The identifier for the CClient_Name Variable.
        /// </summary>
        public const uint CClient_Name = 13;

        /// <summary>
        /// The identifier for the CClient_Adress Variable.
        /// </summary>
        public const uint CClient_Adress = 14;

        /// <summary>
        /// The identifier for the CEvidenceEntry_Amount Variable.
        /// </summary>
        public const uint CEvidenceEntry_Amount = 17;

        /// <summary>
        /// The identifier for the CEvidenceEntry_Product_ID Variable.
        /// </summary>
        public const uint CEvidenceEntry_Product_ID = 23;

        /// <summary>
        /// The identifier for the CEvidenceEntry_Product_Name Variable.
        /// </summary>
        public const uint CEvidenceEntry_Product_Name = 24;

        /// <summary>
        /// The identifier for the CEvidenceEntry_Product_Price Variable.
        /// </summary>
        public const uint CEvidenceEntry_Product_Price = 25;

        /// <summary>
        /// The identifier for the CProduct_ID Variable.
        /// </summary>
        public const uint CProduct_ID = 19;

        /// <summary>
        /// The identifier for the CProduct_Name Variable.
        /// </summary>
        public const uint CProduct_Name = 20;

        /// <summary>
        /// The identifier for the CProduct_Price Variable.
        /// </summary>
        public const uint CProduct_Price = 21;

        /// <summary>
        /// The identifier for the COrder_ID Variable.
        /// </summary>
        public const uint COrder_ID = 26;

        /// <summary>
        /// The identifier for the COrder_Client_ID Variable.
        /// </summary>
        public const uint COrder_Client_ID = 28;

        /// <summary>
        /// The identifier for the COrder_Client_Name Variable.
        /// </summary>
        public const uint COrder_Client_Name = 29;

        /// <summary>
        /// The identifier for the COrder_Client_Adress Variable.
        /// </summary>
        public const uint COrder_Client_Adress = 30;

        /// <summary>
        /// The identifier for the COrder_Entries_Amount Variable.
        /// </summary>
        public const uint COrder_Entries_Amount = 32;

        /// <summary>
        /// The identifier for the COrder_Entries_Product_ID Variable.
        /// </summary>
        public const uint COrder_Entries_Product_ID = 34;

        /// <summary>
        /// The identifier for the COrder_Entries_Product_Name Variable.
        /// </summary>
        public const uint COrder_Entries_Product_Name = 35;

        /// <summary>
        /// The identifier for the COrder_Entries_Product_Price Variable.
        /// </summary>
        public const uint COrder_Entries_Product_Price = 36;

        /// <summary>
        /// The identifier for the CSendRequest_Type Variable.
        /// </summary>
        public const uint CSendRequest_Type = 38;

        /// <summary>
        /// The identifier for the CSendRequest_RequestedID Variable.
        /// </summary>
        public const uint CSendRequest_RequestedID = 39;

        /// <summary>
        /// The identifier for the CSubscribeUpdates_Subscribe Variable.
        /// </summary>
        public const uint CSubscribeUpdates_Subscribe = 41;

        /// <summary>
        /// The identifier for the CSubscribeUpdates_CycleInSeconds Variable.
        /// </summary>
        public const uint CSubscribeUpdates_CycleInSeconds = 42;
    }
    #endregion

    #region Object Node Identifiers
    /// <summary>
    /// A class that declares constants for all Objects in the Model Design.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public static partial class ObjectIds
    {
        /// <summary>
        /// The identifier for the CEvidenceEntry_Product Object.
        /// </summary>
        public static readonly ExpandedNodeId CEvidenceEntry_Product = new ExpandedNodeId(TPUM.OPC.Objects.CEvidenceEntry_Product, TPUM.OPC.Namespaces.TPUM_OPC);

        /// <summary>
        /// The identifier for the COrder_Client Object.
        /// </summary>
        public static readonly ExpandedNodeId COrder_Client = new ExpandedNodeId(TPUM.OPC.Objects.COrder_Client, TPUM.OPC.Namespaces.TPUM_OPC);

        /// <summary>
        /// The identifier for the COrder_Entries Object.
        /// </summary>
        public static readonly ExpandedNodeId COrder_Entries = new ExpandedNodeId(TPUM.OPC.Objects.COrder_Entries, TPUM.OPC.Namespaces.TPUM_OPC);

        /// <summary>
        /// The identifier for the COrder_Entries_Product Object.
        /// </summary>
        public static readonly ExpandedNodeId COrder_Entries_Product = new ExpandedNodeId(TPUM.OPC.Objects.COrder_Entries_Product, TPUM.OPC.Namespaces.TPUM_OPC);
    }
    #endregion

    #region ObjectType Node Identifiers
    /// <summary>
    /// A class that declares constants for all ObjectTypes in the Model Design.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public static partial class ObjectTypeIds
    {
        /// <summary>
        /// The identifier for the CClient ObjectType.
        /// </summary>
        public static readonly ExpandedNodeId CClient = new ExpandedNodeId(TPUM.OPC.ObjectTypes.CClient, TPUM.OPC.Namespaces.TPUM_OPC);

        /// <summary>
        /// The identifier for the CEvidenceEntry ObjectType.
        /// </summary>
        public static readonly ExpandedNodeId CEvidenceEntry = new ExpandedNodeId(TPUM.OPC.ObjectTypes.CEvidenceEntry, TPUM.OPC.Namespaces.TPUM_OPC);

        /// <summary>
        /// The identifier for the CProduct ObjectType.
        /// </summary>
        public static readonly ExpandedNodeId CProduct = new ExpandedNodeId(TPUM.OPC.ObjectTypes.CProduct, TPUM.OPC.Namespaces.TPUM_OPC);

        /// <summary>
        /// The identifier for the COrder ObjectType.
        /// </summary>
        public static readonly ExpandedNodeId COrder = new ExpandedNodeId(TPUM.OPC.ObjectTypes.COrder, TPUM.OPC.Namespaces.TPUM_OPC);

        /// <summary>
        /// The identifier for the CSendRequest ObjectType.
        /// </summary>
        public static readonly ExpandedNodeId CSendRequest = new ExpandedNodeId(TPUM.OPC.ObjectTypes.CSendRequest, TPUM.OPC.Namespaces.TPUM_OPC);

        /// <summary>
        /// The identifier for the CSubscribeUpdates ObjectType.
        /// </summary>
        public static readonly ExpandedNodeId CSubscribeUpdates = new ExpandedNodeId(TPUM.OPC.ObjectTypes.CSubscribeUpdates, TPUM.OPC.Namespaces.TPUM_OPC);
    }
    #endregion

    #region Variable Node Identifiers
    /// <summary>
    /// A class that declares constants for all Variables in the Model Design.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public static partial class VariableIds
    {
        /// <summary>
        /// The identifier for the CClient_ID Variable.
        /// </summary>
        public static readonly ExpandedNodeId CClient_ID = new ExpandedNodeId(TPUM.OPC.Variables.CClient_ID, TPUM.OPC.Namespaces.TPUM_OPC);

        /// <summary>
        /// The identifier for the CClient_Name Variable.
        /// </summary>
        public static readonly ExpandedNodeId CClient_Name = new ExpandedNodeId(TPUM.OPC.Variables.CClient_Name, TPUM.OPC.Namespaces.TPUM_OPC);

        /// <summary>
        /// The identifier for the CClient_Adress Variable.
        /// </summary>
        public static readonly ExpandedNodeId CClient_Adress = new ExpandedNodeId(TPUM.OPC.Variables.CClient_Adress, TPUM.OPC.Namespaces.TPUM_OPC);

        /// <summary>
        /// The identifier for the CEvidenceEntry_Amount Variable.
        /// </summary>
        public static readonly ExpandedNodeId CEvidenceEntry_Amount = new ExpandedNodeId(TPUM.OPC.Variables.CEvidenceEntry_Amount, TPUM.OPC.Namespaces.TPUM_OPC);

        /// <summary>
        /// The identifier for the CEvidenceEntry_Product_ID Variable.
        /// </summary>
        public static readonly ExpandedNodeId CEvidenceEntry_Product_ID = new ExpandedNodeId(TPUM.OPC.Variables.CEvidenceEntry_Product_ID, TPUM.OPC.Namespaces.TPUM_OPC);

        /// <summary>
        /// The identifier for the CEvidenceEntry_Product_Name Variable.
        /// </summary>
        public static readonly ExpandedNodeId CEvidenceEntry_Product_Name = new ExpandedNodeId(TPUM.OPC.Variables.CEvidenceEntry_Product_Name, TPUM.OPC.Namespaces.TPUM_OPC);

        /// <summary>
        /// The identifier for the CEvidenceEntry_Product_Price Variable.
        /// </summary>
        public static readonly ExpandedNodeId CEvidenceEntry_Product_Price = new ExpandedNodeId(TPUM.OPC.Variables.CEvidenceEntry_Product_Price, TPUM.OPC.Namespaces.TPUM_OPC);

        /// <summary>
        /// The identifier for the CProduct_ID Variable.
        /// </summary>
        public static readonly ExpandedNodeId CProduct_ID = new ExpandedNodeId(TPUM.OPC.Variables.CProduct_ID, TPUM.OPC.Namespaces.TPUM_OPC);

        /// <summary>
        /// The identifier for the CProduct_Name Variable.
        /// </summary>
        public static readonly ExpandedNodeId CProduct_Name = new ExpandedNodeId(TPUM.OPC.Variables.CProduct_Name, TPUM.OPC.Namespaces.TPUM_OPC);

        /// <summary>
        /// The identifier for the CProduct_Price Variable.
        /// </summary>
        public static readonly ExpandedNodeId CProduct_Price = new ExpandedNodeId(TPUM.OPC.Variables.CProduct_Price, TPUM.OPC.Namespaces.TPUM_OPC);

        /// <summary>
        /// The identifier for the COrder_ID Variable.
        /// </summary>
        public static readonly ExpandedNodeId COrder_ID = new ExpandedNodeId(TPUM.OPC.Variables.COrder_ID, TPUM.OPC.Namespaces.TPUM_OPC);

        /// <summary>
        /// The identifier for the COrder_Client_ID Variable.
        /// </summary>
        public static readonly ExpandedNodeId COrder_Client_ID = new ExpandedNodeId(TPUM.OPC.Variables.COrder_Client_ID, TPUM.OPC.Namespaces.TPUM_OPC);

        /// <summary>
        /// The identifier for the COrder_Client_Name Variable.
        /// </summary>
        public static readonly ExpandedNodeId COrder_Client_Name = new ExpandedNodeId(TPUM.OPC.Variables.COrder_Client_Name, TPUM.OPC.Namespaces.TPUM_OPC);

        /// <summary>
        /// The identifier for the COrder_Client_Adress Variable.
        /// </summary>
        public static readonly ExpandedNodeId COrder_Client_Adress = new ExpandedNodeId(TPUM.OPC.Variables.COrder_Client_Adress, TPUM.OPC.Namespaces.TPUM_OPC);

        /// <summary>
        /// The identifier for the COrder_Entries_Amount Variable.
        /// </summary>
        public static readonly ExpandedNodeId COrder_Entries_Amount = new ExpandedNodeId(TPUM.OPC.Variables.COrder_Entries_Amount, TPUM.OPC.Namespaces.TPUM_OPC);

        /// <summary>
        /// The identifier for the COrder_Entries_Product_ID Variable.
        /// </summary>
        public static readonly ExpandedNodeId COrder_Entries_Product_ID = new ExpandedNodeId(TPUM.OPC.Variables.COrder_Entries_Product_ID, TPUM.OPC.Namespaces.TPUM_OPC);

        /// <summary>
        /// The identifier for the COrder_Entries_Product_Name Variable.
        /// </summary>
        public static readonly ExpandedNodeId COrder_Entries_Product_Name = new ExpandedNodeId(TPUM.OPC.Variables.COrder_Entries_Product_Name, TPUM.OPC.Namespaces.TPUM_OPC);

        /// <summary>
        /// The identifier for the COrder_Entries_Product_Price Variable.
        /// </summary>
        public static readonly ExpandedNodeId COrder_Entries_Product_Price = new ExpandedNodeId(TPUM.OPC.Variables.COrder_Entries_Product_Price, TPUM.OPC.Namespaces.TPUM_OPC);

        /// <summary>
        /// The identifier for the CSendRequest_Type Variable.
        /// </summary>
        public static readonly ExpandedNodeId CSendRequest_Type = new ExpandedNodeId(TPUM.OPC.Variables.CSendRequest_Type, TPUM.OPC.Namespaces.TPUM_OPC);

        /// <summary>
        /// The identifier for the CSendRequest_RequestedID Variable.
        /// </summary>
        public static readonly ExpandedNodeId CSendRequest_RequestedID = new ExpandedNodeId(TPUM.OPC.Variables.CSendRequest_RequestedID, TPUM.OPC.Namespaces.TPUM_OPC);

        /// <summary>
        /// The identifier for the CSubscribeUpdates_Subscribe Variable.
        /// </summary>
        public static readonly ExpandedNodeId CSubscribeUpdates_Subscribe = new ExpandedNodeId(TPUM.OPC.Variables.CSubscribeUpdates_Subscribe, TPUM.OPC.Namespaces.TPUM_OPC);

        /// <summary>
        /// The identifier for the CSubscribeUpdates_CycleInSeconds Variable.
        /// </summary>
        public static readonly ExpandedNodeId CSubscribeUpdates_CycleInSeconds = new ExpandedNodeId(TPUM.OPC.Variables.CSubscribeUpdates_CycleInSeconds, TPUM.OPC.Namespaces.TPUM_OPC);
    }
    #endregion

    #region BrowseName Declarations
    /// <summary>
    /// Declares all of the BrowseNames used in the Model Design.
    /// </summary>
    public static partial class BrowseNames
    {
        /// <summary>
        /// The BrowseName for the Adress component.
        /// </summary>
        public const string Adress = "Adress";

        /// <summary>
        /// The BrowseName for the Amount component.
        /// </summary>
        public const string Amount = "Amount";

        /// <summary>
        /// The BrowseName for the CClient component.
        /// </summary>
        public const string CClient = "CClient";

        /// <summary>
        /// The BrowseName for the CEvidenceEntry component.
        /// </summary>
        public const string CEvidenceEntry = "CEvidenceEntry";

        /// <summary>
        /// The BrowseName for the Client component.
        /// </summary>
        public const string Client = "Client";

        /// <summary>
        /// The BrowseName for the COrder component.
        /// </summary>
        public const string COrder = "COrder";

        /// <summary>
        /// The BrowseName for the CProduct component.
        /// </summary>
        public const string CProduct = "CProduct";

        /// <summary>
        /// The BrowseName for the CSendRequest component.
        /// </summary>
        public const string CSendRequest = "CSendRequest";

        /// <summary>
        /// The BrowseName for the CSubscribeUpdates component.
        /// </summary>
        public const string CSubscribeUpdates = "CSubscribeUpdates";

        /// <summary>
        /// The BrowseName for the CycleInSeconds component.
        /// </summary>
        public const string CycleInSeconds = "CycleInSeconds";

        /// <summary>
        /// The BrowseName for the Entries component.
        /// </summary>
        public const string Entries = "Entries";

        /// <summary>
        /// The BrowseName for the ID component.
        /// </summary>
        public const string ID = "ID";

        /// <summary>
        /// The BrowseName for the Name component.
        /// </summary>
        public const string Name = "Name";

        /// <summary>
        /// The BrowseName for the Price component.
        /// </summary>
        public const string Price = "Price";

        /// <summary>
        /// The BrowseName for the Product component.
        /// </summary>
        public const string Product = "Product";

        /// <summary>
        /// The BrowseName for the RequestedID component.
        /// </summary>
        public const string RequestedID = "RequestedID";

        /// <summary>
        /// The BrowseName for the Subscribe component.
        /// </summary>
        public const string Subscribe = "Subscribe";

        /// <summary>
        /// The BrowseName for the Type component.
        /// </summary>
        public const string Type = "Type";
    }
    #endregion

    #region Namespace Declarations
    /// <summary>
    /// Defines constants for all namespaces referenced by the model design.
    /// </summary>
    public static partial class Namespaces
    {
        /// <summary>
        /// The URI for the TPUM_OPC namespace (.NET code namespace is 'TPUM.OPC').
        /// </summary>
        public const string TPUM_OPC = "http://gm.com/shop/opc/";

        /// <summary>
        /// The URI for the OpcUa namespace (.NET code namespace is 'Opc.Ua').
        /// </summary>
        public const string OpcUa = "http://opcfoundation.org/UA/";

        /// <summary>
        /// The URI for the OpcUaXsd namespace (.NET code namespace is 'Opc.Ua').
        /// </summary>
        public const string OpcUaXsd = "http://opcfoundation.org/UA/2008/02/Types.xsd";
    }
    #endregion

    #region CClientState Class
    #if (!OPCUA_EXCLUDE_CClientState)
    /// <summary>
    /// Stores an instance of the CClient ObjectType.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public partial class CClientState : BaseObjectState
    {
        #region Constructors
        /// <summary>
        /// Initializes the type with its default attribute values.
        /// </summary>
        public CClientState(NodeState parent) : base(parent)
        {
        }

        /// <summary>
        /// Returns the id of the default type definition node for the instance.
        /// </summary>
        protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(TPUM.OPC.ObjectTypes.CClient, TPUM.OPC.Namespaces.TPUM_OPC, namespaceUris);
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        /// <summary>
        /// Initializes the instance.
        /// </summary>
        protected override void Initialize(ISystemContext context)
        {
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        protected override void Initialize(ISystemContext context, NodeState source)
        {
            InitializeOptionalChildren(context);
            base.Initialize(context, source);
        }

        /// <summary>
        /// Initializes the any option children defined for the instance.
        /// </summary>
        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

        #region Initialization String
        private const string InitializationString =
           "AQAAABcAAABodHRwOi8vZ20uY29tL3Nob3Avb3BjL/////8EYIAAAQAAAAEADwAAAENDbGllbnRJbnN0" +
           "YW5jZQEBCwABAQsA/////wMAAAAVYIkKAgAAAAEAAgAAAElEAQEMAAAvAD8MAAAAABv/////AQH/////" +
           "AAAAABVgiQoCAAAAAQAEAAAATmFtZQEBDQAALwA/DQAAAAAM/////wEB/////wAAAAAVYIkKAgAAAAEA" +
           "BgAAAEFkcmVzcwEBDgAALwA/DgAAAAAM/////wEB/////wAAAAA=";
        #endregion
        #endif
        #endregion

        #region Public Properties
        /// <summary>
        /// A description for the ID Variable.
        /// </summary>
        public BaseDataVariableState ID
        {
            get
            {
                return m_iD;
            }

            set
            {
                if (!Object.ReferenceEquals(m_iD, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_iD = value;
            }
        }

        /// <summary>
        /// A description for the Name Variable.
        /// </summary>
        public BaseDataVariableState<string> Name
        {
            get
            {
                return m_name;
            }

            set
            {
                if (!Object.ReferenceEquals(m_name, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_name = value;
            }
        }

        /// <summary>
        /// A description for the Adress Variable.
        /// </summary>
        public BaseDataVariableState<string> Adress
        {
            get
            {
                return m_adress;
            }

            set
            {
                if (!Object.ReferenceEquals(m_adress, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_adress = value;
            }
        }
        #endregion

        #region Overridden Methods
        /// <summary>
        /// Populates a list with the children that belong to the node.
        /// </summary>
        /// <param name="context">The context for the system being accessed.</param>
        /// <param name="children">The list of children to populate.</param>
        public override void GetChildren(
            ISystemContext context,
            IList<BaseInstanceState> children)
        {
            if (m_iD != null)
            {
                children.Add(m_iD);
            }

            if (m_name != null)
            {
                children.Add(m_name);
            }

            if (m_adress != null)
            {
                children.Add(m_adress);
            }

            base.GetChildren(context, children);
        }

        /// <summary>
        /// Finds the child with the specified browse name.
        /// </summary>
        protected override BaseInstanceState FindChild(
            ISystemContext context,
            QualifiedName browseName,
            bool createOrReplace,
            BaseInstanceState replacement)
        {
            if (QualifiedName.IsNull(browseName))
            {
                return null;
            }

            BaseInstanceState instance = null;

            switch (browseName.Name)
            {
                case TPUM.OPC.BrowseNames.ID:
                {
                    if (createOrReplace)
                    {
                        if (ID == null)
                        {
                            if (replacement == null)
                            {
                                ID = new BaseDataVariableState(this);
                            }
                            else
                            {
                                ID = (BaseDataVariableState)replacement;
                            }
                        }
                    }

                    instance = ID;
                    break;
                }

                case TPUM.OPC.BrowseNames.Name:
                {
                    if (createOrReplace)
                    {
                        if (Name == null)
                        {
                            if (replacement == null)
                            {
                                Name = new BaseDataVariableState<string>(this);
                            }
                            else
                            {
                                Name = (BaseDataVariableState<string>)replacement;
                            }
                        }
                    }

                    instance = Name;
                    break;
                }

                case TPUM.OPC.BrowseNames.Adress:
                {
                    if (createOrReplace)
                    {
                        if (Adress == null)
                        {
                            if (replacement == null)
                            {
                                Adress = new BaseDataVariableState<string>(this);
                            }
                            else
                            {
                                Adress = (BaseDataVariableState<string>)replacement;
                            }
                        }
                    }

                    instance = Adress;
                    break;
                }
            }

            if (instance != null)
            {
                return instance;
            }

            return base.FindChild(context, browseName, createOrReplace, replacement);
        }
        #endregion

        #region Private Fields
        private BaseDataVariableState m_iD;
        private BaseDataVariableState<string> m_name;
        private BaseDataVariableState<string> m_adress;
        #endregion
    }
    #endif
    #endregion

    #region CEvidenceEntryState Class
    #if (!OPCUA_EXCLUDE_CEvidenceEntryState)
    /// <summary>
    /// Stores an instance of the CEvidenceEntry ObjectType.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public partial class CEvidenceEntryState : BaseObjectState
    {
        #region Constructors
        /// <summary>
        /// Initializes the type with its default attribute values.
        /// </summary>
        public CEvidenceEntryState(NodeState parent) : base(parent)
        {
        }

        /// <summary>
        /// Returns the id of the default type definition node for the instance.
        /// </summary>
        protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(TPUM.OPC.ObjectTypes.CEvidenceEntry, TPUM.OPC.Namespaces.TPUM_OPC, namespaceUris);
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        /// <summary>
        /// Initializes the instance.
        /// </summary>
        protected override void Initialize(ISystemContext context)
        {
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        protected override void Initialize(ISystemContext context, NodeState source)
        {
            InitializeOptionalChildren(context);
            base.Initialize(context, source);
        }

        /// <summary>
        /// Initializes the any option children defined for the instance.
        /// </summary>
        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

        #region Initialization String
        private const string InitializationString =
           "AQAAABcAAABodHRwOi8vZ20uY29tL3Nob3Avb3BjL/////8EYIAAAQAAAAEAFgAAAENFdmlkZW5jZUVu" +
           "dHJ5SW5zdGFuY2UBAQ8AAQEPAP////8CAAAAFWCJCgIAAAABAAYAAABBbW91bnQBAREAAC8APxEAAAAA" +
           "G/////8BAf////8AAAAABGCACgEAAAABAAcAAABQcm9kdWN0AQEQAAAvAQESABAAAAD/////AwAAABVg" +
           "iQoCAAAAAQACAAAASUQBARcAAC8APxcAAAAAG/////8BAf////8AAAAAFWCJCgIAAAABAAQAAABOYW1l" +
           "AQEYAAAvAD8YAAAAAAz/////AQH/////AAAAABVgiQoCAAAAAQAFAAAAUHJpY2UBARkAAC8APxkAAAAA" +
           "Mv////8BAf////8AAAAA";
        #endregion
        #endif
        #endregion

        #region Public Properties
        /// <summary>
        /// A description for the Amount Variable.
        /// </summary>
        public BaseDataVariableState Amount
        {
            get
            {
                return m_amount;
            }

            set
            {
                if (!Object.ReferenceEquals(m_amount, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_amount = value;
            }
        }

        /// <summary>
        /// A description for the Product Object.
        /// </summary>
        public CProductState Product
        {
            get
            {
                return m_product;
            }

            set
            {
                if (!Object.ReferenceEquals(m_product, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_product = value;
            }
        }
        #endregion

        #region Overridden Methods
        /// <summary>
        /// Populates a list with the children that belong to the node.
        /// </summary>
        /// <param name="context">The context for the system being accessed.</param>
        /// <param name="children">The list of children to populate.</param>
        public override void GetChildren(
            ISystemContext context,
            IList<BaseInstanceState> children)
        {
            if (m_amount != null)
            {
                children.Add(m_amount);
            }

            if (m_product != null)
            {
                children.Add(m_product);
            }

            base.GetChildren(context, children);
        }

        /// <summary>
        /// Finds the child with the specified browse name.
        /// </summary>
        protected override BaseInstanceState FindChild(
            ISystemContext context,
            QualifiedName browseName,
            bool createOrReplace,
            BaseInstanceState replacement)
        {
            if (QualifiedName.IsNull(browseName))
            {
                return null;
            }

            BaseInstanceState instance = null;

            switch (browseName.Name)
            {
                case TPUM.OPC.BrowseNames.Amount:
                {
                    if (createOrReplace)
                    {
                        if (Amount == null)
                        {
                            if (replacement == null)
                            {
                                Amount = new BaseDataVariableState(this);
                            }
                            else
                            {
                                Amount = (BaseDataVariableState)replacement;
                            }
                        }
                    }

                    instance = Amount;
                    break;
                }

                case TPUM.OPC.BrowseNames.Product:
                {
                    if (createOrReplace)
                    {
                        if (Product == null)
                        {
                            if (replacement == null)
                            {
                                Product = new CProductState(this);
                            }
                            else
                            {
                                Product = (CProductState)replacement;
                            }
                        }
                    }

                    instance = Product;
                    break;
                }
            }

            if (instance != null)
            {
                return instance;
            }

            return base.FindChild(context, browseName, createOrReplace, replacement);
        }
        #endregion

        #region Private Fields
        private BaseDataVariableState m_amount;
        private CProductState m_product;
        #endregion
    }
    #endif
    #endregion

    #region CProductState Class
    #if (!OPCUA_EXCLUDE_CProductState)
    /// <summary>
    /// Stores an instance of the CProduct ObjectType.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public partial class CProductState : BaseObjectState
    {
        #region Constructors
        /// <summary>
        /// Initializes the type with its default attribute values.
        /// </summary>
        public CProductState(NodeState parent) : base(parent)
        {
        }

        /// <summary>
        /// Returns the id of the default type definition node for the instance.
        /// </summary>
        protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(TPUM.OPC.ObjectTypes.CProduct, TPUM.OPC.Namespaces.TPUM_OPC, namespaceUris);
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        /// <summary>
        /// Initializes the instance.
        /// </summary>
        protected override void Initialize(ISystemContext context)
        {
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        protected override void Initialize(ISystemContext context, NodeState source)
        {
            InitializeOptionalChildren(context);
            base.Initialize(context, source);
        }

        /// <summary>
        /// Initializes the any option children defined for the instance.
        /// </summary>
        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

        #region Initialization String
        private const string InitializationString =
           "AQAAABcAAABodHRwOi8vZ20uY29tL3Nob3Avb3BjL/////8EYIAAAQAAAAEAEAAAAENQcm9kdWN0SW5z" +
           "dGFuY2UBARIAAQESAP////8DAAAAFWCJCgIAAAABAAIAAABJRAEBEwAALwA/EwAAAAAb/////wEB////" +
           "/wAAAAAVYIkKAgAAAAEABAAAAE5hbWUBARQAAC8APxQAAAAADP////8BAf////8AAAAAFWCJCgIAAAAB" +
           "AAUAAABQcmljZQEBFQAALwA/FQAAAAAy/////wEB/////wAAAAA=";
        #endregion
        #endif
        #endregion

        #region Public Properties
        /// <summary>
        /// A description for the ID Variable.
        /// </summary>
        public BaseDataVariableState ID
        {
            get
            {
                return m_iD;
            }

            set
            {
                if (!Object.ReferenceEquals(m_iD, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_iD = value;
            }
        }

        /// <summary>
        /// A description for the Name Variable.
        /// </summary>
        public BaseDataVariableState<string> Name
        {
            get
            {
                return m_name;
            }

            set
            {
                if (!Object.ReferenceEquals(m_name, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_name = value;
            }
        }

        /// <summary>
        /// A description for the Price Variable.
        /// </summary>
        public BaseDataVariableState Price
        {
            get
            {
                return m_price;
            }

            set
            {
                if (!Object.ReferenceEquals(m_price, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_price = value;
            }
        }
        #endregion

        #region Overridden Methods
        /// <summary>
        /// Populates a list with the children that belong to the node.
        /// </summary>
        /// <param name="context">The context for the system being accessed.</param>
        /// <param name="children">The list of children to populate.</param>
        public override void GetChildren(
            ISystemContext context,
            IList<BaseInstanceState> children)
        {
            if (m_iD != null)
            {
                children.Add(m_iD);
            }

            if (m_name != null)
            {
                children.Add(m_name);
            }

            if (m_price != null)
            {
                children.Add(m_price);
            }

            base.GetChildren(context, children);
        }

        /// <summary>
        /// Finds the child with the specified browse name.
        /// </summary>
        protected override BaseInstanceState FindChild(
            ISystemContext context,
            QualifiedName browseName,
            bool createOrReplace,
            BaseInstanceState replacement)
        {
            if (QualifiedName.IsNull(browseName))
            {
                return null;
            }

            BaseInstanceState instance = null;

            switch (browseName.Name)
            {
                case TPUM.OPC.BrowseNames.ID:
                {
                    if (createOrReplace)
                    {
                        if (ID == null)
                        {
                            if (replacement == null)
                            {
                                ID = new BaseDataVariableState(this);
                            }
                            else
                            {
                                ID = (BaseDataVariableState)replacement;
                            }
                        }
                    }

                    instance = ID;
                    break;
                }

                case TPUM.OPC.BrowseNames.Name:
                {
                    if (createOrReplace)
                    {
                        if (Name == null)
                        {
                            if (replacement == null)
                            {
                                Name = new BaseDataVariableState<string>(this);
                            }
                            else
                            {
                                Name = (BaseDataVariableState<string>)replacement;
                            }
                        }
                    }

                    instance = Name;
                    break;
                }

                case TPUM.OPC.BrowseNames.Price:
                {
                    if (createOrReplace)
                    {
                        if (Price == null)
                        {
                            if (replacement == null)
                            {
                                Price = new BaseDataVariableState(this);
                            }
                            else
                            {
                                Price = (BaseDataVariableState)replacement;
                            }
                        }
                    }

                    instance = Price;
                    break;
                }
            }

            if (instance != null)
            {
                return instance;
            }

            return base.FindChild(context, browseName, createOrReplace, replacement);
        }
        #endregion

        #region Private Fields
        private BaseDataVariableState m_iD;
        private BaseDataVariableState<string> m_name;
        private BaseDataVariableState m_price;
        #endregion
    }
    #endif
    #endregion

    #region COrderState Class
    #if (!OPCUA_EXCLUDE_COrderState)
    /// <summary>
    /// Stores an instance of the COrder ObjectType.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public partial class COrderState : BaseObjectState
    {
        #region Constructors
        /// <summary>
        /// Initializes the type with its default attribute values.
        /// </summary>
        public COrderState(NodeState parent) : base(parent)
        {
        }

        /// <summary>
        /// Returns the id of the default type definition node for the instance.
        /// </summary>
        protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(TPUM.OPC.ObjectTypes.COrder, TPUM.OPC.Namespaces.TPUM_OPC, namespaceUris);
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        /// <summary>
        /// Initializes the instance.
        /// </summary>
        protected override void Initialize(ISystemContext context)
        {
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        protected override void Initialize(ISystemContext context, NodeState source)
        {
            InitializeOptionalChildren(context);
            base.Initialize(context, source);
        }

        /// <summary>
        /// Initializes the any option children defined for the instance.
        /// </summary>
        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

        #region Initialization String
        private const string InitializationString =
           "AQAAABcAAABodHRwOi8vZ20uY29tL3Nob3Avb3BjL/////8EYIAAAQAAAAEADgAAAENPcmRlckluc3Rh" +
           "bmNlAQEWAAEBFgD/////AwAAABVgiQoCAAAAAQACAAAASUQBARoAAC8APxoAAAAAG/////8BAf////8A" +
           "AAAABGCACgEAAAABAAYAAABDbGllbnQBARsAAC8BAQsAGwAAAP////8DAAAAFWCJCgIAAAABAAIAAABJ" +
           "RAEBHAAALwA/HAAAAAAb/////wEB/////wAAAAAVYIkKAgAAAAEABAAAAE5hbWUBAR0AAC8APx0AAAAA" +
           "DP////8BAf////8AAAAAFWCJCgIAAAABAAYAAABBZHJlc3MBAR4AAC8APx4AAAAADP////8BAf////8A" +
           "AAAABGCACgEAAAABAAcAAABFbnRyaWVzAQEfAAAvAQEPAB8AAAD/////AgAAABVgiQoCAAAAAQAGAAAA" +
           "QW1vdW50AQEgAAAvAD8gAAAAABv/////AQH/////AAAAAARggAoBAAAAAQAHAAAAUHJvZHVjdAEBIQAA" +
           "LwEBEgAhAAAA/////wMAAAAVYIkKAgAAAAEAAgAAAElEAQEiAAAvAD8iAAAAABv/////AQH/////AAAA" +
           "ABVgiQoCAAAAAQAEAAAATmFtZQEBIwAALwA/IwAAAAAM/////wEB/////wAAAAAVYIkKAgAAAAEABQAA" +
           "AFByaWNlAQEkAAAvAD8kAAAAADL/////AQH/////AAAAAA==";
        #endregion
        #endif
        #endregion

        #region Public Properties
        /// <summary>
        /// A description for the ID Variable.
        /// </summary>
        public BaseDataVariableState ID
        {
            get
            {
                return m_iD;
            }

            set
            {
                if (!Object.ReferenceEquals(m_iD, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_iD = value;
            }
        }

        /// <summary>
        /// A description for the Client Object.
        /// </summary>
        public CClientState Client
        {
            get
            {
                return m_client;
            }

            set
            {
                if (!Object.ReferenceEquals(m_client, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_client = value;
            }
        }

        /// <summary>
        /// A description for the Entries Object.
        /// </summary>
        public CEvidenceEntryState Entries
        {
            get
            {
                return m_entries;
            }

            set
            {
                if (!Object.ReferenceEquals(m_entries, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_entries = value;
            }
        }
        #endregion

        #region Overridden Methods
        /// <summary>
        /// Populates a list with the children that belong to the node.
        /// </summary>
        /// <param name="context">The context for the system being accessed.</param>
        /// <param name="children">The list of children to populate.</param>
        public override void GetChildren(
            ISystemContext context,
            IList<BaseInstanceState> children)
        {
            if (m_iD != null)
            {
                children.Add(m_iD);
            }

            if (m_client != null)
            {
                children.Add(m_client);
            }

            if (m_entries != null)
            {
                children.Add(m_entries);
            }

            base.GetChildren(context, children);
        }

        /// <summary>
        /// Finds the child with the specified browse name.
        /// </summary>
        protected override BaseInstanceState FindChild(
            ISystemContext context,
            QualifiedName browseName,
            bool createOrReplace,
            BaseInstanceState replacement)
        {
            if (QualifiedName.IsNull(browseName))
            {
                return null;
            }

            BaseInstanceState instance = null;

            switch (browseName.Name)
            {
                case TPUM.OPC.BrowseNames.ID:
                {
                    if (createOrReplace)
                    {
                        if (ID == null)
                        {
                            if (replacement == null)
                            {
                                ID = new BaseDataVariableState(this);
                            }
                            else
                            {
                                ID = (BaseDataVariableState)replacement;
                            }
                        }
                    }

                    instance = ID;
                    break;
                }

                case TPUM.OPC.BrowseNames.Client:
                {
                    if (createOrReplace)
                    {
                        if (Client == null)
                        {
                            if (replacement == null)
                            {
                                Client = new CClientState(this);
                            }
                            else
                            {
                                Client = (CClientState)replacement;
                            }
                        }
                    }

                    instance = Client;
                    break;
                }

                case TPUM.OPC.BrowseNames.Entries:
                {
                    if (createOrReplace)
                    {
                        if (Entries == null)
                        {
                            if (replacement == null)
                            {
                                Entries = new CEvidenceEntryState(this);
                            }
                            else
                            {
                                Entries = (CEvidenceEntryState)replacement;
                            }
                        }
                    }

                    instance = Entries;
                    break;
                }
            }

            if (instance != null)
            {
                return instance;
            }

            return base.FindChild(context, browseName, createOrReplace, replacement);
        }
        #endregion

        #region Private Fields
        private BaseDataVariableState m_iD;
        private CClientState m_client;
        private CEvidenceEntryState m_entries;
        #endregion
    }
    #endif
    #endregion

    #region CSendRequestState Class
    #if (!OPCUA_EXCLUDE_CSendRequestState)
    /// <summary>
    /// Stores an instance of the CSendRequest ObjectType.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public partial class CSendRequestState : BaseObjectState
    {
        #region Constructors
        /// <summary>
        /// Initializes the type with its default attribute values.
        /// </summary>
        public CSendRequestState(NodeState parent) : base(parent)
        {
        }

        /// <summary>
        /// Returns the id of the default type definition node for the instance.
        /// </summary>
        protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(TPUM.OPC.ObjectTypes.CSendRequest, TPUM.OPC.Namespaces.TPUM_OPC, namespaceUris);
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        /// <summary>
        /// Initializes the instance.
        /// </summary>
        protected override void Initialize(ISystemContext context)
        {
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        protected override void Initialize(ISystemContext context, NodeState source)
        {
            InitializeOptionalChildren(context);
            base.Initialize(context, source);
        }

        /// <summary>
        /// Initializes the any option children defined for the instance.
        /// </summary>
        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

        #region Initialization String
        private const string InitializationString =
           "AQAAABcAAABodHRwOi8vZ20uY29tL3Nob3Avb3BjL/////8EYIAAAQAAAAEAFAAAAENTZW5kUmVxdWVz" +
           "dEluc3RhbmNlAQElAAEBJQD/////AgAAABVgiQoCAAAAAQAEAAAAVHlwZQEBJgAALwA/JgAAAAAM////" +
           "/wEB/////wAAAAAVYIkKAgAAAAEACwAAAFJlcXVlc3RlZElEAQEnAAAvAD8nAAAAABv/////AQH/////" +
           "AAAAAA==";
        #endregion
        #endif
        #endregion

        #region Public Properties
        /// <summary>
        /// A description for the Type Variable.
        /// </summary>
        public BaseDataVariableState<string> Type
        {
            get
            {
                return m_type;
            }

            set
            {
                if (!Object.ReferenceEquals(m_type, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_type = value;
            }
        }

        /// <summary>
        /// A description for the RequestedID Variable.
        /// </summary>
        public BaseDataVariableState RequestedID
        {
            get
            {
                return m_requestedID;
            }

            set
            {
                if (!Object.ReferenceEquals(m_requestedID, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_requestedID = value;
            }
        }
        #endregion

        #region Overridden Methods
        /// <summary>
        /// Populates a list with the children that belong to the node.
        /// </summary>
        /// <param name="context">The context for the system being accessed.</param>
        /// <param name="children">The list of children to populate.</param>
        public override void GetChildren(
            ISystemContext context,
            IList<BaseInstanceState> children)
        {
            if (m_type != null)
            {
                children.Add(m_type);
            }

            if (m_requestedID != null)
            {
                children.Add(m_requestedID);
            }

            base.GetChildren(context, children);
        }

        /// <summary>
        /// Finds the child with the specified browse name.
        /// </summary>
        protected override BaseInstanceState FindChild(
            ISystemContext context,
            QualifiedName browseName,
            bool createOrReplace,
            BaseInstanceState replacement)
        {
            if (QualifiedName.IsNull(browseName))
            {
                return null;
            }

            BaseInstanceState instance = null;

            switch (browseName.Name)
            {
                case TPUM.OPC.BrowseNames.Type:
                {
                    if (createOrReplace)
                    {
                        if (Type == null)
                        {
                            if (replacement == null)
                            {
                                Type = new BaseDataVariableState<string>(this);
                            }
                            else
                            {
                                Type = (BaseDataVariableState<string>)replacement;
                            }
                        }
                    }

                    instance = Type;
                    break;
                }

                case TPUM.OPC.BrowseNames.RequestedID:
                {
                    if (createOrReplace)
                    {
                        if (RequestedID == null)
                        {
                            if (replacement == null)
                            {
                                RequestedID = new BaseDataVariableState(this);
                            }
                            else
                            {
                                RequestedID = (BaseDataVariableState)replacement;
                            }
                        }
                    }

                    instance = RequestedID;
                    break;
                }
            }

            if (instance != null)
            {
                return instance;
            }

            return base.FindChild(context, browseName, createOrReplace, replacement);
        }
        #endregion

        #region Private Fields
        private BaseDataVariableState<string> m_type;
        private BaseDataVariableState m_requestedID;
        #endregion
    }
    #endif
    #endregion

    #region CSubscribeUpdatesState Class
    #if (!OPCUA_EXCLUDE_CSubscribeUpdatesState)
    /// <summary>
    /// Stores an instance of the CSubscribeUpdates ObjectType.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public partial class CSubscribeUpdatesState : BaseObjectState
    {
        #region Constructors
        /// <summary>
        /// Initializes the type with its default attribute values.
        /// </summary>
        public CSubscribeUpdatesState(NodeState parent) : base(parent)
        {
        }

        /// <summary>
        /// Returns the id of the default type definition node for the instance.
        /// </summary>
        protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(TPUM.OPC.ObjectTypes.CSubscribeUpdates, TPUM.OPC.Namespaces.TPUM_OPC, namespaceUris);
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        /// <summary>
        /// Initializes the instance.
        /// </summary>
        protected override void Initialize(ISystemContext context)
        {
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        protected override void Initialize(ISystemContext context, NodeState source)
        {
            InitializeOptionalChildren(context);
            base.Initialize(context, source);
        }

        /// <summary>
        /// Initializes the any option children defined for the instance.
        /// </summary>
        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

        #region Initialization String
        private const string InitializationString =
           "AQAAABcAAABodHRwOi8vZ20uY29tL3Nob3Avb3BjL/////8EYIAAAQAAAAEAGQAAAENTdWJzY3JpYmVV" +
           "cGRhdGVzSW5zdGFuY2UBASgAAQEoAP////8CAAAAFWCJCgIAAAABAAkAAABTdWJzY3JpYmUBASkAAC8A" +
           "PykAAAAAAf////8BAf////8AAAAAFWCJCgIAAAABAA4AAABDeWNsZUluU2Vjb25kcwEBKgAALwA/KgAA" +
           "AAAb/////wEB/////wAAAAA=";
        #endregion
        #endif
        #endregion

        #region Public Properties
        /// <summary>
        /// A description for the Subscribe Variable.
        /// </summary>
        public BaseDataVariableState<bool> Subscribe
        {
            get
            {
                return m_subscribe;
            }

            set
            {
                if (!Object.ReferenceEquals(m_subscribe, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_subscribe = value;
            }
        }

        /// <summary>
        /// A description for the CycleInSeconds Variable.
        /// </summary>
        public BaseDataVariableState CycleInSeconds
        {
            get
            {
                return m_cycleInSeconds;
            }

            set
            {
                if (!Object.ReferenceEquals(m_cycleInSeconds, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_cycleInSeconds = value;
            }
        }
        #endregion

        #region Overridden Methods
        /// <summary>
        /// Populates a list with the children that belong to the node.
        /// </summary>
        /// <param name="context">The context for the system being accessed.</param>
        /// <param name="children">The list of children to populate.</param>
        public override void GetChildren(
            ISystemContext context,
            IList<BaseInstanceState> children)
        {
            if (m_subscribe != null)
            {
                children.Add(m_subscribe);
            }

            if (m_cycleInSeconds != null)
            {
                children.Add(m_cycleInSeconds);
            }

            base.GetChildren(context, children);
        }

        /// <summary>
        /// Finds the child with the specified browse name.
        /// </summary>
        protected override BaseInstanceState FindChild(
            ISystemContext context,
            QualifiedName browseName,
            bool createOrReplace,
            BaseInstanceState replacement)
        {
            if (QualifiedName.IsNull(browseName))
            {
                return null;
            }

            BaseInstanceState instance = null;

            switch (browseName.Name)
            {
                case TPUM.OPC.BrowseNames.Subscribe:
                {
                    if (createOrReplace)
                    {
                        if (Subscribe == null)
                        {
                            if (replacement == null)
                            {
                                Subscribe = new BaseDataVariableState<bool>(this);
                            }
                            else
                            {
                                Subscribe = (BaseDataVariableState<bool>)replacement;
                            }
                        }
                    }

                    instance = Subscribe;
                    break;
                }

                case TPUM.OPC.BrowseNames.CycleInSeconds:
                {
                    if (createOrReplace)
                    {
                        if (CycleInSeconds == null)
                        {
                            if (replacement == null)
                            {
                                CycleInSeconds = new BaseDataVariableState(this);
                            }
                            else
                            {
                                CycleInSeconds = (BaseDataVariableState)replacement;
                            }
                        }
                    }

                    instance = CycleInSeconds;
                    break;
                }
            }

            if (instance != null)
            {
                return instance;
            }

            return base.FindChild(context, browseName, createOrReplace, replacement);
        }
        #endregion

        #region Private Fields
        private BaseDataVariableState<bool> m_subscribe;
        private BaseDataVariableState m_cycleInSeconds;
        #endregion
    }
    #endif
    #endregion
}