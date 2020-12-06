using System;
using System.Collections.Generic;

namespace AlgorithmsReview.DataStructures {
    class BinarySearchTree {

        private Node _rootNode;

        public BinarySearchTree(Node rootNode = null) {
            _rootNode = rootNode;
        }

        public void InsertNode(int key) {

            if (_rootNode == null) {
                _rootNode = new Node(key);
                return;
            }

            Insert(key, _rootNode);

        }

        private void Insert(int key, Node parentNode) {

            if (key < parentNode.Key) {

                if (parentNode.LeftNode == null) {
                    parentNode.LeftNode = new Node(key);
                    return;
                }

                Insert(key, parentNode.LeftNode);
                return;

            }

            if (parentNode.RightNode == null) {
                parentNode.RightNode = new Node(key);
                return;
            }

            Insert(key, parentNode.RightNode);

        }

        public void DeleteNode(int key) {

            if (_rootNode == null) {
                return;
            }

            Delete(key, _rootNode, null);

        }

        private void Delete(int key, Node node, Node parentNode) {

            if (node.Key == key) {

                var leftNode = node.LeftNode == null ? null : new Node(node.LeftNode);
                var rightNode = node.RightNode == null ? null : new Node(node.RightNode);

                if (parentNode is null) {

                    _rootNode = null;

                } else {

                    if (parentNode.LeftNode == node) {
                        parentNode.LeftNode = null;
                    } else {
                        parentNode.RightNode = null;
                    }
                }

                InsertAllNodes(leftNode);
                InsertAllNodes(rightNode);

                return;

            }

            if (key < node.Key) {

                if (node.LeftNode is null) {
                    return;
                }

                Delete(key, node.LeftNode, node);

                return;
            }

            if (node.RightNode is null) {
                return;
            }

            Delete(key, node.RightNode, node);

        }

        private void InsertAllNodes(Node node) {

            if (node == null) {
                return;
            }

            InsertNode(node.Key);
            InsertAllNodes(node.LeftNode);
            InsertAllNodes(node.RightNode);

        }

        public int Max() {

            if (_rootNode == null) {
                throw new Exception("There is no node in the BST");
            }

            return TraverseToTheMostRight(_rootNode);

        }

        private int TraverseToTheMostRight(Node node) {

            if (node.RightNode == null) {
                return node.Key;
            }

            return TraverseToTheMostRight(node.RightNode);

        }

        public int Min() {

            if (_rootNode == null) {
                throw new Exception("There is no node in the BST");
            }

            return TraverseToTheMostLeft(_rootNode);

        }

        private int TraverseToTheMostLeft(Node node) {

            if (node.LeftNode == null) {
                return node.Key;
            }

            return TraverseToTheMostLeft(node.LeftNode);

        }

        void PrintPostOrder(Node node) {

            if (node == null)
                return;

            // first recur on left subtree 
            PrintPostOrder(node.LeftNode);

            // then recur on right subtree 
            PrintPostOrder(node.RightNode);

            // now deal with the node 
            Console.Write(node.Key + " ");

        }

        void PrintInOrder(Node node) {
            if (node == null)
                return;

            /* first recur on left child */
            PrintInOrder(node.LeftNode);

            /* then print the data of node */
            Console.Write(node.Key + " ");

            /* now recur on right child */
            PrintInOrder(node.RightNode);

        }

        void PrintPreOrder(Node node) {
            if (node == null)
                return;

            /* first print data of node */
            Console.Write(node.Key + " ");

            /* then recur on left sutree */
            PrintPreOrder(node.LeftNode);

            /* now recur on right subtree */
            PrintPreOrder(node.RightNode);

        }

        public void PrintBst() {
            if (_rootNode is null) {
                Console.WriteLine("No BST Data.");
                return;
            }

            _rootNode.PrintPretty("", true);

        }
    }
    class Node {
        public int Key { get; set; }
        public Node LeftNode { get; set; }
        public Node RightNode { get; set; }

        public Node(int key) {
            Key = key;
        }

        public Node(Node node) {
            Key = node.Key;
            LeftNode = node.LeftNode;
            RightNode = node.RightNode;
        }

        public void PrintPretty(string indent, bool last) {

            Console.Write(indent);
            if (last) {
                Console.Write("└─");
                indent += "  ";
            } else {
                Console.Write("├─");
                indent += "| ";
            }
            Console.WriteLine(Key);

            var children = new List<Node>();
            if (LeftNode != null)
                children.Add(LeftNode);
            if (RightNode != null)
                children.Add(RightNode);

            for (int i = 0; i < children.Count; i++)
                children[i].PrintPretty(indent, i == children.Count - 1);

        }
    }
}
