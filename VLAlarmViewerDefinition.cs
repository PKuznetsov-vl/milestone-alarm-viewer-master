using AlarmPreview.Client;
using VLAlarmViewer.Admin;
using System;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;
using VideoOS.Platform;
using VideoOS.Platform.Admin;

namespace VLAlarmViewer
{
    /// <summary>
    /// The PluginDefinition is the ‘entry’ point to any plugin.  
    /// This is the starting point for any plugin development and the class MUST be available for a plugin to be loaded.  
    /// Several PluginDefinitions are allowed to be available within one DLL.
    /// Here the references to all other plugin known objects and classes are defined.
    /// The class is an abstract class where all implemented methods and properties need to be declared with override.
    /// The class is constructed when the environment is loading the DLL.
    /// </summary>
    public class VLAlarmViewerDefinition : PluginDefinition
    {
        private static System.Drawing.Image _treeNodeImage;
        private static System.Drawing.Image _topTreeNodeImage;

        internal static Guid VLAlarmViewerPluginId = new Guid("f17c1ef4-8793-4674-9ac3-3fc4a8c28a36");
        internal static Guid VLAlarmViewerPluginKind = new Guid("381e7bd1-8a18-40fe-b143-94beeea6cbe9");

        private List<ItemNode> _itemNodes = new List<ItemNode>();
   
        #region Initialization

        /// <summary>
        /// Load resources 
        /// </summary>
        static VLAlarmViewerDefinition()
        {
            _treeNodeImage = Properties.Resources.DummyItem;
            _topTreeNodeImage = Properties.Resources.Server;
        }


        /// <summary>
        /// Get the icon for the plugin
        /// </summary>
        internal static Image TreeNodeImage
        {
            get { return _treeNodeImage; }
        }

        #endregion

        /// <summary>
        /// This method is called when the environment is up and running.
        /// Registration of Messages via RegisterReceiver can be done at this point.
        /// </summary>
        public override void Init()
        {
            _itemNodes = new List<ItemNode>
            {
                new ItemNode(
                    VLAlarmViewerPluginKind,
                    Guid.Empty,
                    "Camera configuration", _treeNodeImage,
                    "Camera configuration", _treeNodeImage,
                    Category.Text, true,
                    ItemsAllowed.None,
                    new VLAlarmViewerItemManager(),
                    null
                    )
            };
        }

        /// <summary>
        /// The main application is about to be in an undetermined state, either logging off or exiting.
        /// You can release resources at this point, it should match what you acquired during Init, so additional call to Init() will work.
        /// </summary>
        public override void Close()
        {
            _itemNodes.Clear();
        }

        #region Identification Properties

        /// <summary>
        /// Gets the unique id identifying this plugin component
        /// </summary>
        public override Guid Id
        {
            get
            {
                return VLAlarmViewerPluginId;
            }
        }

        /// <summary>
        /// Define name of top level Tree node - e.g. A product name
        /// </summary>
        public override string Name
        {
            get { return "VisionLabs Face Recognition"; }
        }

        /// <summary>
        /// Your company name
        /// </summary>
        public override string Manufacturer
        {
            get
            {
                return "VisionLabs B.V.";
            }
        }

        /// <summary>
        /// Version of this plugin.
        /// </summary>
        public override string VersionString
        {
            get
            {
                return "1.0.0.1";
            }
        }

        /// <summary>
        /// Icon to be used on top level - e.g. a product or company logo
        /// </summary>
        public override System.Drawing.Image Icon
        {
            get { return _topTreeNodeImage; }
        }

        #endregion

        #region Client properties

        /// <summary>
        /// Override GenerateAlarmPreviewUserControl causes the usercontrol to be called when
        /// the Smart Client user marks an Alarm line in the AlarmList control
        /// The usercontrol is then automatically included in the AlarmPreview control
        /// </summary>
        /// <param name="alarmOrBaseEvent"></param>
        /// <returns></returns>
        public override System.Windows.Controls.UserControl GenerateAlarmPreviewWpfUserControl(object alarmOrBaseEvent)
        {
            return new AlarmPreviewWpfUserControl(alarmOrBaseEvent);
        }

        #endregion

        #region Administration properties

        /// <summary>
        /// A list of server side configuration items in the administrator
        /// </summary>
        public override List<ItemNode> ItemNodes
        {
            get { return _itemNodes; }
        }

        /// <summary>
        /// A user control to display when the administrator clicks on the top TreeNode
        /// </summary>
        public override UserControl GenerateUserControl()
        {
            return new OverviewPage();
        }

        /// <summary>
        /// This property can be set to true, to be able to display your own help UserControl on the entire panel.
        /// When this is false - a standard top and left side is added by the system.
        /// </summary>
        public override bool UserControlFillEntirePanel
        {
            get { return false; }
        }
        
        #endregion
    }
}
