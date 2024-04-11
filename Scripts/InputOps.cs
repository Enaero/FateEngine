using System;
using System.Linq.Expressions;
using Godot;

public static class InputOps
{
    public const string POINTER_SELECT = "pointer_select";

#nullable enable
    public static Node? GetClickedNode3D(Camera3D camera, float maxDistance)
    {
        if (Input.IsActionJustPressed(POINTER_SELECT))
        {
            Vector2 mousePosition = camera.GetViewport().GetMousePosition();
            Vector3 cameraOrigin = camera.ProjectRayOrigin(mousePosition);
            Vector3 direction = camera.ProjectRayNormal(mousePosition);

            PhysicsDirectSpaceState3D space = camera.GetWorld3D().DirectSpaceState;
            PhysicsRayQueryParameters3D query = new()
            {
                From = cameraOrigin,
                To = cameraOrigin + direction * maxDistance,
                CollideWithAreas = true,
                CollideWithBodies = true
            };
            Godot.Collections.Dictionary collisions = space.IntersectRay(query);
            
            if (!collisions.TryGetValue("collider", out Variant collidingNode))
            {
                // No collision.
                return null;
            }

            GD.Print("Collided with", collidingNode);
            return collidingNode.As<Node>();
        }

        return null;
    }
#nullable disable
}