namespace Model
{
    public class Constants
    {
        /// <summary>
        /// The speed a walker character will move
        /// </summary>
        public const float SPEED_WALKER = 0.005f;

        /// <summary>
        /// The lower bounds of a GameObject's Y position before
        /// we kill it. 
        /// </summary>
        public const int DEATH_Y = -3;

        public const int NUM_WALKERS = 1;
        public const int SPAWN_DELAY = 2;

        public const string PREFAB_LVL01 = "Prefabs/level01";
        public const string PREFAB_WALKER = "Prefabs/walker01";

        public const string TILE_SPAWN = "Gameboy Tileset_0";
        public const string TILE_GOAL = "Gameboy Tileset_55";
    }
}