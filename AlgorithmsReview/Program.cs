using System;
using System.Collections.Generic;

namespace AlgorithmsReview {
    class Program {
        static void Main(string[] args) {

            int[] targetArray = { 1, -10, 25, 19, 30, 35, 101, 100, 0 };
            Console.WriteLine(string.Join(", ", targetArray));

            Console.WriteLine(LinearSearch(targetArray, 1000));
            Console.WriteLine(LinearSearch(targetArray, 25));

            int[] mergedArray = MergeSort(targetArray);
            Console.WriteLine(string.Join(", ", mergedArray));

            Console.WriteLine(BinarySearch(mergedArray, 1000));
            Console.WriteLine(BinarySearch(mergedArray, 35));

            Console.WriteLine(string.Join(", ", targetArray));

            BubbleSort(targetArray);
            Console.WriteLine(string.Join(", ", targetArray));

            targetArray = new[] { 1, -10, 25, 19, 30, 35, 101, 100, 0 };
            Console.WriteLine(string.Join(", ", targetArray));

            InsertionSort(targetArray);
            Console.WriteLine(string.Join(", ", targetArray));

            var bst = new BinarySearchTree();
            bst.PrintBst();

            bst.InsertNode(10);
            bst.InsertNode(50);
            bst.InsertNode(5);
            bst.InsertNode(55);
            bst.InsertNode(45);

            bst.PrintBst();

            bst.DeleteNode(0);
            bst.PrintBst();

            bst.DeleteNode(50);
            bst.PrintBst();

            bst.DeleteNode(10);
            bst.PrintBst();

            bst.InsertNode(10);
            bst.InsertNode(-1);
            bst.PrintBst();


            var graph = new Graph<int>();
            for (int i = 1; i < 11; i++) {
                graph.AddVertex(i);
            }
            graph.AddEdge(1, 2);
            graph.AddEdge(1, 3);
            graph.AddEdge(2, 4);
            graph.AddEdge(3, 5);
            graph.AddEdge(3, 6);
            graph.AddEdge(4, 7);
            graph.AddEdge(5, 7);
            graph.AddEdge(5, 8);
            graph.AddEdge(5, 6);
            graph.AddEdge(8, 9);
            graph.AddEdge(9, 10);

            Console.WriteLine(string.Join(", ", graph.BreadthFirstSearch(1)));
            Console.WriteLine(string.Join(", ", graph.DepthFirstSearch(1)));

            DepthFirstSearchUsingRecursion(graph, 1);
            Console.WriteLine(string.Join(", ", _visited));

            Console.WriteLine(graph.ShortestPath(0, 10));

            /*You're given a two-dimensional array (a matrix) of potentially unequal height and width containing only 0s and 1s.
             Each 0 represents land, and each 1 represents part of a river.
             A river consists of any number of 1s that are either horizontally or vertically adjacent (but not diagonally adjacent).
             The number of adjacent 1s forming a river determine its size.
             Note that a river can twist.
             In other words, it doesn't have to be a straight vertical line or a straight horizontal line;
             it can be L-shaped, for example.
             Write a function that returns an array fo the sizes of all rivers represented in the input matrix.
             The sizes don't need to be in any particular order*/
            int[,] matrix = {
                {1, 0, 0, 1, 0},
                {1, 0, 1, 0, 0},
                {0, 0, 1, 0, 1},
                {1, 0, 1, 0, 1},
                {1, 0, 1, 1, 0}
            };

            var result = RiverSizes(matrix);
            /*Expected Output
             [1, 2, 2, 2, 5]
             The numbers could be ordered differently.*/
            Console.WriteLine(string.Join(", ", result.ToArray()));

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
                    if (endIndex < startIndex) {
                        break;
                    }
                }else {
                    startIndex = midIndex + 1;
                    if (startIndex > endIndex) {
                        break;
                    }
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

        private static LinkedList<object> _visited = new LinkedList<object>();
        public static void DepthFirstSearchUsingRecursion<T>(Graph<T> graph, T start) {

            if (!graph.Adjecency.ContainsKey(start)) {
                return;
            }

            if (_visited.Contains(start)) {
                return;
            }

            _visited.AddLast(start);

            foreach (T edge in graph.Adjecency[start]) {
                if (_visited.Contains(edge)) {
                    continue;
                }
                DepthFirstSearchUsingRecursion(graph, edge);
            }
        }

        private static List<int> RiverSizes(int[,] matrix) {

            var riverSizes = new List<int>();

            if (matrix is null) {
                return riverSizes;
            }

            int rowsNumber = matrix.GetLength(0);
            int columnsNumber = matrix.GetLength(1);

            bool[,] visited = new bool[rowsNumber, columnsNumber];

            for (int i = 0; i < rowsNumber; i++) {

                for (int j = 0; j < columnsNumber; j++) {

                    if (visited[i, j]) {
                        continue;
                    }

                    var riverSize = TraverseMatrix(i, j, matrix, visited);
                    if (riverSize > 0) {
                        riverSizes.Add(riverSize);
                    }
                }
            }

            return riverSizes;

        }

        private static int TraverseMatrix(int i, int j, int[,] matrix, bool[,] visited) {

            int riverSize = 0;

            Stack<(int, int)> stack = new Stack<(int, int)>();
            stack.Push((i, j));

            while (stack.Count > 0) {

                var vertex = stack.Pop();
                if (visited[vertex.Item1, vertex.Item2]) {
                    continue;
                }

                visited[vertex.Item1, vertex.Item2] = true;

                if (matrix[vertex.Item1, vertex.Item2] == 0) {
                    continue;
                }

                riverSize++;

                var neighbors = GetVertexNeighbors(vertex.Item1, vertex.Item2, matrix);
                foreach ((int, int) item in neighbors) {

                    if (!visited[item.Item1, item.Item2]) {
                        stack.Push((item.Item1, item.Item2));
                    }
                }
            }

            return riverSize;

        }

        private static List<(int, int)> GetVertexNeighbors(int i, int j, int[,] matrix) {

            var neighbors = new List<(int, int)>();
            var rowsNumber = matrix.GetLength(0);
            var columnsNumber = matrix.GetLength(1);

            if (i > 0) {
                neighbors.Add((i - 1, j));
            }

            if (i < rowsNumber - 1) {
                neighbors.Add((i + 1, j));
            }

            if (j > 0) {
                neighbors.Add((i, j - 1));
            }

            if (j < columnsNumber - 1) {
                neighbors.Add((i, j + 1));
            }

            return neighbors;

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

        public void PrintBst() {
            if (_rootNode is null) {
                Console.WriteLine("No BST Data.");
                return;
            }

            _rootNode.PrintPretty("", true);

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

        //O(V+E)
        //O(V) Space Complexity
        public LinkedList<T> BreadthFirstSearch(T start) {

            var visited = new LinkedList<T>();
            if (!Adjecency.ContainsKey(start)) {
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

                foreach (T item in Adjecency[vertex]) {

                    if (!visited.Contains(item)) {
                        queue.Enqueue(item);
                    }
                }
            }

            return visited;

        }

        //O(V+E)
        //O(V) Space Complexity
        public LinkedList<T> DepthFirstSearch(T start) {

            var visited = new LinkedList<T>();
            if (!Adjecency.ContainsKey(start)) {
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

                foreach (T item in Adjecency[vertex]) {

                    if (!visited.Contains(item)) {
                        stack.Push(item);
                    }
                }
            }

            return visited;

        }

        public string ShortestPath(T startVertex, T endVertex) {

            if (!Adjecency.ContainsKey(startVertex) || !Adjecency.ContainsKey(endVertex)) {
                return string.Empty;
            }

            var visited = new List<T>();
            Queue<(T, string)> queue = new Queue<(T, string)>();
            queue.Enqueue((startVertex, startVertex.ToString()));

            while (queue.Count > 0) {

                var vertex = queue.Dequeue();
                if (visited.Contains(vertex.Item1)) {
                    continue;
                }
                visited.Add(vertex.Item1);

                if (Equals(vertex.Item1, endVertex)) {
                    return vertex.Item2;
                }

                foreach (T edge in Adjecency[vertex.Item1]) {

                    if (visited.Contains(edge)) {
                        continue;
                    }

                    queue.Enqueue((edge, vertex.Item2 + ", " + edge.ToString()));

                }
            }

            return string.Empty;

        }
    }
}
