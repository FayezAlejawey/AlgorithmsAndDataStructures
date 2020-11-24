using System;
using System.Collections.Generic;

namespace AlgorithmsReview {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Hello World!");
        }

        //O(n)
        private static int LinearSearch(int[] arr, int value) {

            if (arr == null || arr.Length < 1) {
                return -1;
            }

            for (int i = 0; i < arr.Length; i++) {

                if (arr[i] == value) {
                    return i;
                }
            }

            return -1;

        }

        //Required a sorted array
        //O(log(n))
        private static int BinarySearch(int[] sortedArr, int value) {

            if (sortedArr == null || sortedArr.Length < 1) {
                return -1;
            }

            int startIndex = 0;
            int endIndex = sortedArr.Length - 1;

            for (int i = 0; i < sortedArr.Length; i++) {

                int midIndex = startIndex + (endIndex - startIndex) / 2;

                if (sortedArr[midIndex] == value) {
                    return midIndex;
                }

                if (sortedArr[midIndex] > value) {
                    endIndex = midIndex - 1;
                }else {
                    startIndex = midIndex + 1;
                }
            }

            return -1;

        }

        //Required Sorted equally distributed array
        //O(log(log(n)))
        private static int InterpolationSearch(int[] sortedArr, int value) {

            if (sortedArr == null || sortedArr.Length < 1) {
                return -1;
            }

            int startIndex = 0;
            int endIndex = sortedArr.Length - 1;

            for (int i = 0; i < sortedArr.Length; i++) {

                int midIndex = startIndex + ((endIndex - startIndex) / (sortedArr[endIndex] - sortedArr[startIndex])) *
                                             (value - sortedArr[startIndex]);

                if (sortedArr[midIndex] == value) {
                    return midIndex;
                }

                if (sortedArr[midIndex] > value) {
                    endIndex = midIndex - 1;
                } else {
                    startIndex = midIndex + 1;
                }
            }

            return -1;

        }

        //O(n^2)
        private static void BubbleSort(int[] arr) {

            if (arr == null || arr.Length < 2) {
                return;
            }

            for (int i = 0; i < arr.Length; i++) {
                
                bool isSwapped = false;

                for (int j = 0; j < arr.Length; j++) {

                    if (j < arr.Length - 1 && arr[j] > arr[j + 1]) {
                        (arr[j], arr[j + 1]) = (arr[j + 1], arr[j]);
                        isSwapped = true;
                    }
                }

                if (!isSwapped) {
                    break;
                }
            }
        }

        //O(n^2)
        private static void InsertionSort(int[] arr) {

            if (arr == null || arr.Length < 2) {
                return;
            }

            for (int i = 1; i < arr.Length; i++) {

                for (int j = i; j > 0; j--) {

                    if (arr[j] > arr[j - 1]) {
                        (arr[j], arr[j - 1]) = (arr[j - 1], arr[j]);
                    }
                }
            }
        }

        //O(n*log(n))
        private static int[] MergeSort(int[] arr) {

            if (arr == null || arr.Length < 2) {
                return arr;
            }

            int midIndex = arr.Length / 2;
            int[] leftArr = new int[midIndex];
            int[] rightArr = new int[arr.Length - midIndex];

            for (int i = 0; i < arr.Length; i++) {
                if (i < midIndex) {
                    leftArr[i] = arr[i];
                }else {
                    rightArr[i - midIndex] = arr[i];
                }
            }

            leftArr = MergeSort(leftArr);
            rightArr = MergeSort(rightArr);

            int[] resultArr = Merge(leftArr, rightArr);

            return resultArr;

        }

        private static int[] Merge(int[] left, int[] right) {

            int[] result = new int[left.Length + right.Length];

            int leftIndex = 0, rightIndex = 0, resultIndex = 0;

            while (leftIndex < left.Length || rightIndex < right.Length) {

                if (leftIndex < left.Length && rightIndex < right.Length) {

                    if (left[leftIndex] < right[rightIndex]) {
                        result[resultIndex] = left[leftIndex];
                        leftIndex++;
                        resultIndex++;
                    }else {
                        result[resultIndex] = right[rightIndex];
                        rightIndex++;
                        resultIndex++;
                    }
                }else if (leftIndex < left.Length) {
                    result[resultIndex] = left[leftIndex];
                    leftIndex++;
                    resultIndex++;
                } else if (rightIndex < right.Length) {
                    result[resultIndex] = right[rightIndex];
                    rightIndex++;
                    resultIndex++;
                }
            }

            return result;

        }

        //O(V+E)
        private static LinkedList<T> BreadthFirstSearch<T>(Graph<T> graph,T start) {

            LinkedList<T> visited = new LinkedList<T>();

            if (!graph.Adjecency.ContainsKey(start)) {
                return visited;
            }

            Queue<T> queue = new Queue<T>();
            queue.Enqueue(start);

            while (queue.Count > 0) {

                T vertex = queue.Dequeue();

                if (visited.Contains(vertex)) {
                    continue;
                }

                visited.AddLast(vertex);

                foreach (T edge in graph.Adjecency[vertex]) {

                    if (!visited.Contains(edge)) {
                        queue.Enqueue(edge);
                    }
                }
            }

            return visited;

        }

        //O(V+E)
        private static LinkedList<T> DepthFirstSearch<T>(Graph<T> graph, T start) {

            LinkedList<T> visited = new LinkedList<T>();

            if (!graph.Adjecency.ContainsKey(start)) {
                return visited;
            }

            Stack<T> stack = new Stack<T>();
            stack.Push(start);

            while (stack.Count > 0) {

                T vertex = stack.Pop();

                if (visited.Contains(vertex)) {
                    continue;
                }

                visited.AddLast(vertex);

                foreach (T edge in graph.Adjecency[vertex]) {

                    if (!visited.Contains(edge)) {
                        stack.Push(edge);
                    }
                }
            }

            return visited;

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
    }
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

            if (node == null) {
                return;
            }

            if (key < node.Key) {
                Delete(key, node.LeftNode, node);
                return;
            }

            if (key > node.Key) {
                Delete(key, node.RightNode, node);
                return;
            }

            Node leftNode = null;
            Node rightNode = null;

            if (node.LeftNode != null && node.RightNode != null) {
                leftNode = new Node(node.LeftNode);
                rightNode = new Node(node.RightNode);
            }else if (node.LeftNode != null) {
                leftNode = new Node(node.LeftNode);
            }else if (node.RightNode != null) {
                rightNode = new Node(node.RightNode);
            }

            if (parentNode == null) {
                _rootNode = null;
            }else {
                if (parentNode.LeftNode == node) {
                    parentNode.LeftNode = null;
                } else {
                    parentNode.RightNode = null;
                }
            }

            InsertAllNodes(leftNode);
            InsertAllNodes(rightNode);

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
    }
    class Graph<T> {

        public Dictionary<T,LinkedList<T>> Adjecency { get; } = new Dictionary<T, LinkedList<T>>();

        public void AddVertex(T vertex) {
            Adjecency[vertex] = new LinkedList<T>();
        }

        public void AddEdge(T fromVertex, T toVertex) {

            if (Adjecency.ContainsKey(fromVertex) && Adjecency.ContainsKey(toVertex)) {
                Adjecency[fromVertex].AddLast(toVertex);
            }
        }
    }
}
