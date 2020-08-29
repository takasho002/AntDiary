using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace AntDiary.Editor
{
    public partial class GeneTreeEditorWindow : EditorWindow
    {
        /*
        [MenuItem("Window/GeneTreeEditor")]
        private static void Open()
        {
            var windowInstance = CreateInstance<GeneTreeEditor>();
            windowInstance.Show();
        }
        */

        private static Dictionary<GeneTree, EditorWindow> openedTrees = new Dictionary<GeneTree, EditorWindow>();
        private GeneTreeGraphView graphView;

        public static void Open(GeneTree tree)
        {
            if (openedTrees.ContainsKey(tree))
            {
                var w = openedTrees[tree];
                if (w)
                {
                    w.Focus();
                    return;
                }

                openedTrees.Remove(tree);
            }

            var windowInstance = CreateInstance<GeneTreeEditorWindow>();
            windowInstance.Initialize(tree);
            windowInstance.Show();
        }

        public void Initialize(GeneTree tree)
        {
            target = tree;
            BuildVisualElement();
            openedTrees.Add(target, this);
            this.titleContent = new GUIContent($"GeneTree Editor ({target.name})");
        }

        void OnEnable()
        {
            if (!target) return;
            openedTrees.Add(target, this);
            BuildVisualElement();
        }

        private void OnDestroy()
        {
            graphView?.Save();
        }

        private void BuildVisualElement()
        {
            rootVisualElement.Clear();
            var vt = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(
                "Assets/AntDiary/Scripts/System/Editor/GeneTreeEditor.uxml");
            var root = vt.CloneTree();
            graphView = new GeneTreeGraphView(this, target);
            graphView.style.flexGrow = 1f;
            root.Q("GraphContainer").Add(graphView);
            root.Q<Button>("SaveButton").clicked += () => graphView.Save();
            root.Q<Button>("SelectAssetButton").clicked += () =>
            {
                Selection.SetActiveObjectWithContext(target, target);
            };
            root.style.flexGrow = 1f;
            rootVisualElement.Add(root);
        }

        [SerializeField] private GeneTree target;
    }

    public class GeneTreeGraphView : GraphView
    {
        private GeneTree target;

        public GeneTreeGraphView(EditorWindow host, GeneTree target) : base()
        {
            SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
            Insert(0, new GridBackground());
            this.AddManipulator(new SelectionDragger());

            nodeCreationRequest += context =>
            {
                var n = new GeneNode(AddGeneToTree());
                var p = context.screenMousePosition;
                p -= host.position.position;

                n.SetPosition(new Rect(p.x, p.y, 0, 0));
                AddElement(n);
            };

            //ロード
            this.target = target;
            //ノード
            if (target.Genes != null)
                foreach (var gene in target.Genes)
                {
                    var n = new GeneNode(gene);
                    AddElement(n);
                }

            //接続
            var nodeList = nodes.ToList();
            if (target.Edges != null)
                foreach (var edge in target.Edges)
                {
                    var geneNodes = nodeList.OfType<GeneNode>();
                    var childNode = geneNodes.FirstOrDefault(n => n.TargetGene.Guid == edge.ChildGeneGuid);
                    if (childNode == default) continue;
                    var parentNode = geneNodes.FirstOrDefault(n => n.TargetGene.Guid == edge.ParentGeneGuid);
                    if (parentNode == default) continue;
                    var graphEdge = new Edge()
                    {
                        output = parentNode.OutPort,
                        input = childNode.InPort
                    };
                    graphEdge.input.Connect(graphEdge);
                    graphEdge.output.Connect(graphEdge);
                    AddElement(graphEdge);
                }
        }

        public override List<Port> GetCompatiblePorts(Port startAnchor, NodeAdapter nodeAdapter)
        {
            return ports.ToList().Where(p => p.direction != startAnchor.direction && p.node != startAnchor.node)
                .ToList();
        }

        private Gene AddGeneToTree()
        {
            var g = new Gene();
            target.Genes.Add(g);
            EditorUtility.SetDirty(target);
            return g;
        }

        public void Save()
        {
            //ノードの位置
            nodes.ForEach(n =>
            {
                if (n is GeneNode node)
                {
                    node.TargetGene.Position = node.GetPosition();
                }
            });

            //削除済みノードの除去
            target.Genes.Clear();
            target.Genes.AddRange(nodes.ToList().OfType<GeneNode>()
                .Select(n => n.TargetGene));

            //遺伝子間接続
            target.Edges.Clear();
            edges.ForEach(e =>
            {
                var parentPort = e.output;
                var childPort = e.input;
                if (parentPort.node is GeneNode parent && childPort.node is GeneNode child)
                {
                    var edge = new GeneEdge();
                    edge.ParentGeneGuid = parent.TargetGene.Guid;
                    edge.ChildGeneGuid = child.TargetGene.Guid;
                    target.Edges.Add(edge);
                }
            });
            EditorUtility.SetDirty(target);
        }
    }

    public class GeneNode : UnityEditor.Experimental.GraphView.Node
    {
        public Gene TargetGene { get; private set; }
        public Port InPort { get; }
        public Port OutPort { get; }

        public GeneNode(Gene gene)
        {
            title = "遺伝子";

            InPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(Port));
            InPort.name = "In";
            inputContainer.Add(InPort);

            OutPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(Port));
            OutPort.name = "Out";
            outputContainer.Add(OutPort);

            TargetGene = gene;

            this.style.minWidth = 200;

            var idField = new TextField("ID");
            idField.value = gene.Id;
            idField.RegisterValueChangedCallback(c => TargetGene.Id = c.newValue);
            mainContainer.Add(idField);

            var displayNameField = new TextField("表示名");
            displayNameField.value = gene.DisplayName;
            displayNameField.RegisterValueChangedCallback(c => TargetGene.DisplayName = c.newValue);
            mainContainer.Add(displayNameField);
            displayNameField.RegisterValueChangedCallback(c => title = c.newValue);

            var descriptionField = new TextField("説明");
            descriptionField.multiline = true;
            descriptionField.value = gene.Description;
            descriptionField.RegisterValueChangedCallback(c => TargetGene.Description = c.newValue);
            mainContainer.Add(descriptionField);

            contentContainer.Add(new Label(gene.Guid));

            this.SetPosition(gene.Position);
        }
    }
}