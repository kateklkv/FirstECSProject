namespace Kulikova
{
    public interface ILevelUp
    {
        int MinLevel { get; }
        void LevelUp(CharacterData characterData, int level);
    }
}