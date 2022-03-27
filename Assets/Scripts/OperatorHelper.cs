using System.Collections.Generic;

public class OperatorHelper
{
    public Path TryGetAvailablePath(ISelectable fromSelected, ISelectable toSelected)
    {
        if (fromSelected is Character)
        {
            if (toSelected is Character)
            {
                return TryGetAvailablePath(fromSelected as Character, toSelected as Character);
            }
            else if (toSelected is Node)
            {
                return TryGetAvailablePath(fromSelected as Character, toSelected as Node);
            }
        }
        
        return null;
    }

    public Path TryGetAvailablePath(Character fromCharacter, Character toCharacter)
    {
        Node toNode = toCharacter.GetInteractable() as Node;
        if (toNode != null)
        {
            return TryGetAvailablePath(fromCharacter, toNode);
        }

        return null;
    }

    public Path TryGetAvailablePath(Character fromCharacter, Node toNode)
    {
        Node fromNode = fromCharacter.GetInteractable() as Node;
        if (fromNode != null)
        {
            return TryGetAvailablePath(fromNode, toNode);
        }

        return null;
    }

    public Path TryGetAvailablePath(Node fromNode, Node toNode)
    {
        List<Path> pathFromNode = fromNode.GetPathList();
        foreach (Path path in pathFromNode)
        {
            if (path.GetTargetNode() == toNode)
            {
                return path;
            }
        }

        return null;
    }
}
