namespace AlgorithmsWebAPI.Services
{
    public interface IDFSService
    {
        void Search(string array);
    }
    class TreeNode
    {
        public int Value;
        public TreeNode Left;
        public TreeNode Right;

        public TreeNode(int value)
        {
            Value = value;
            Left = null;
            Right = null;
        }
    }
    public class DFSService : IDFSService
    {
        private TreeNode Root;

        string input = "1(2(4(),5()),3(6(),7()))";

        public void Search(string list)
        {
            Root = BuildTree(input);

            Console.WriteLine("Constructed Tree:");
            DFS();
        }

        private TreeNode BuildTree(string input)
        {
            // Create a stack to help with building the tree
            Stack<TreeNode> stack = new Stack<TreeNode>();
            TreeNode current = null;
            bool isDigit = false;
            int number = 0;

            foreach (char c in input)
            {
                if (char.IsDigit(c))
                {
                    number = number * 10 + (c - '0');
                    isDigit = true;
                }
                else if (c == '(')
                {
                    if (isDigit)
                    {
                        TreeNode newNode = new TreeNode(number);
                        if (current == null)
                        {
                            // The first node encountered becomes the root
                            Root = newNode;
                        }
                        else if (current.Left == null)
                        {
                            current.Left = newNode;
                        }
                        else
                        {
                            current.Right = newNode;
                        }
                        stack.Push(newNode);
                        current = newNode;
                        isDigit = false;
                        number = 0;
                    }
                }
                else if (c == ')')
                {
                    // Pop the current node from the stack
                    stack.Pop();
                    if (stack.Count > 0)
                    {
                        current = stack.Peek();
                    }
                }
            }

            return Root;
        }

        public void DFS()
        {
            Console.WriteLine("Depth First Traversal:");
            DFSUtil(Root);
            Console.WriteLine();
        }

        private void DFSUtil(TreeNode node)
        {
            if (node == null)
            {
                return;
            }

            Console.Write(node.Value + " ");
            DFSUtil(node.Left);
            DFSUtil(node.Right);
        }
    }
}
