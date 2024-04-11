
using System;
using System.Collections.Generic;
using System.Linq;
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

    public static Node FindNode<T>(Node root)
    {
        if (root is T)
        {
            return root;
        }

        List<Node> toSearch = root.GetChildren().ToList();

        while(toSearch.Count > 0)
        {
            Node child = toSearch[^1];
            toSearch.RemoveAt(toSearch.Count - 1);
    
            if (child is T)
            {
                return child;
            }
            toSearch.AddRange(child.GetChildren());
        }

        return null;
    }
    

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
}
