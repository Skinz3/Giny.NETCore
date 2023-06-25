namespace Giny.IO.DLM
{
    public class WorldPoint
    {
        #region Vars
        private uint _mapId;
        private uint _worldId;
        private int _x;
        private int _y;
        #endregion

        #region Properties
        public uint MapId
        {
            get { return _mapId; }
        }

        public uint WorldId
        {
            get { return _worldId; }
        }

        public int X
        {
            get { return _x; }
        }

        public int Y
        {
            get { return _y; }
        }
        #endregion


        public WorldPoint(uint mapId)
        {
            _mapId = mapId;
            _worldId = (_mapId & 1073479680) >> 18;

            _x = (int)(_mapId >> 9 & 511);
            _y = (int)(_mapId & 511);

            if ((_x & 256) == 256)
                _x = -(_x & 255);

            if ((_y & 256) == 256)
                _y = -(_y & 255);
        }
    }
}
