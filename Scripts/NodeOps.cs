
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Godot;

public static class NodeOps 
{
    /// <summary>
    /// Gets the first child found that matches the name, or returns null.
    /// </summary>
    /// <param name="parent">The root node of the operation</param>
    /// <param name="name">The name of the child as it appears in the editor</param>
    /// <returns>The first found child or null</returns>
#nullable enable
    public static T? GetChildByName<T>(this Node parent, string name) 
        where T : Node
    {
        Queue<Node> toSearch = new(parent.GetChildren());

        while (toSearch.Count > 0)
        {
            Node child = toSearch.Dequeue();

            if (child.Name == name)
            {
                return (T) child;
            }

            foreach (Node n in child.GetChildren())
            {
                toSearch.Enqueue(n);
            }
        }

        return null;
    }
#nullable disable

#nullable enable
    /// <summary>
    /// Returns first child node that matches type.
    /// </summary>
    /// <typeparam name="T">A node type</typeparam>
    /// <param name="root">The node to start searching from.</param>
    /// <returns></returns>
    public static T? FindChildByType<T>(this Node root) where T : Node
    {
        if (root is T rootT)
        {
            return rootT;
        }

        List<Node> toSearch = root.GetChildren().ToList();

        while(toSearch.Count > 0)
        {
            Node child = toSearch[^1];
            toSearch.RemoveAt(toSearch.Count - 1);
    
            if (child is T childT)
            {
                return childT;
            }
            toSearch.AddRange(child.GetChildren());
        }

        return null;
    }
#nullable disable

#nullable enable
    public static T? GetSelfOrParentByType<T>(this Node node) where T : Node
    {
        Node candidate = node;
        while (candidate is not null && candidate is not T)
        {
            candidate = candidate.GetParent();
        }

        return candidate as T;
    }
#nullable disable

    public static async Task WaitForReady(this Node waitingNode, Node nodeToWaitFor)
    {
        if (nodeToWaitFor.IsNodeReady())
        {
            GD.Print($"{nodeToWaitFor.Name} is already ready for {waitingNode.Name}");
            return;
        }
        GD.Print($"{waitingNode.Name} starting wait for {nodeToWaitFor.Name}");
        await waitingNode.ToSignal(nodeToWaitFor, Node.SignalName.Ready);
        GD.Print($"{waitingNode.Name} finished wating for {nodeToWaitFor.Name}");
    }
}
