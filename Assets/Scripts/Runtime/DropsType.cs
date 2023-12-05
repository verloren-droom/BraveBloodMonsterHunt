namespace BraveBloodMonsterHunt
{
    public enum DropType
    {
        None,
        Experience,
        Health,
    }

    [System.Serializable]
    public struct DropItem
    {
        public Drop drop;
        public float probability;

        public DropType DropItemType => drop != null ? drop.DropItemType : DropType.None;
    }
}