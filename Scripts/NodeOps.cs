
using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

public class NodeOps 
{
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
}