using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace AntDiary{
    public class Node{

        public int Uid{
            get;
        }

        public Vector2 Pos{
            get;
        }

        private Dictionary<Node, PathParam> _connectedNodes;

        public ReadOnlyDictionary<Node, PathParam> ConnectedNodes => new ReadOnlyDictionary<Node, PathParam>(_connectedNodes);

        internal Node(int uid, Vector2 pos){
            Uid = uid;
            Pos = pos;
            _connectedNodes = new Dictionary<Node, PathParam>();
        }

        internal void Connect(Node node, PathParam pathParam){
            // if(_connectedNodes.ContainsKey(node)){
            //     _connectedNodes.
            // }
            _connectedNodes[node] = pathParam;
            // _connectedNodes.Add(node, pathParam);
        }

        internal void Disconnect(Node node){
            _connectedNodes.Remove(node);
        }
        
        


        public override string ToString(){
            String connStr = "";
            foreach(var pair in _connectedNodes){
                connStr += $"{pair.Key.Uid}({pair.Value.GetCost}), ";
            }
            return $"{Uid}: {Pos.ToString()}   connection:[{connStr}]";
        }
    }
}
