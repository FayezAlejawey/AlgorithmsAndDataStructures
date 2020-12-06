using System.Collections.Generic;

namespace AlgorithmsReview.DataStructures {
    class Graph<T> {

        public Dictionary<T, LinkedList<T>> Adjecency { get; } = new Dictionary<T, LinkedList<T>>();

        public void AddVertex(T vertex) {

            if (Adjecency.ContainsKey(vertex)) {
                return;
            }

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

        //String Version
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

        //List Version
        public List<T> ShortestPathListVersion(T startVertex, T endVertex) {

            var visited = new List<T>();

            if (!Adjecency.ContainsKey(startVertex) || !Adjecency.ContainsKey(endVertex)) {
                return visited;
            }

            var queue = new Queue<(T, List<T>)>();
            queue.Enqueue((startVertex, new List<T> { startVertex }));

            while (queue.Count > 0) {

                var vertex = queue.Dequeue();

                if (Equals(vertex.Item1, endVertex)) {
                    return vertex.Item2;
                }

                visited.Add(vertex.Item1);

                foreach (T v in Adjecency[vertex.Item1]) {
                    if (visited.Contains(v)) {
                        continue;
                    }

                    var lst = new List<T>(vertex.Item2) { v };
                    queue.Enqueue((v, lst));
                }
            }

            return new List<T>();

        }
    }
}
