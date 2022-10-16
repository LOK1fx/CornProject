using UnityEngine;

namespace LOK1game
{
    public class TerrainChecker
    {
        private readonly Terrain _terrain;

        public TerrainChecker(Terrain terrain)
        {
            _terrain = terrain;
        }

        public string GetLayerName(Vector3 playerPosition)
        {
            float[] cellMix = GetTexturesMix(playerPosition);

            var strongest = 0f;
            var maxIndex = 0;

            for (int i = 0; i < cellMix.Length; i++)
            {
                if(cellMix[i] > strongest)
                {
                    maxIndex = i;
                    strongest = cellMix[i];
                }
            }

            return _terrain.terrainData.terrainLayers[maxIndex].name;
        }

        private float[] GetTexturesMix(Vector3 playerPosition)
        {
            var terrainPosition = _terrain.transform.position;
            var terrainData = _terrain.terrainData;

            var mapX = GetAlphaMapPosition(playerPosition.x, terrainPosition.x, terrainData.alphamapWidth);
            var mapZ = GetAlphaMapPosition(playerPosition.y, terrainPosition.y, terrainData.alphamapHeight);
            float[,,] slatMapData = terrainData.GetAlphamaps(mapX, mapZ, 1, 1);

            float[] cellmix = new float[slatMapData.GetUpperBound(2) + 1];

            for (int i = 0; i < cellmix.Length; i++)
            {
                cellmix[i] = slatMapData[0, 0, i];
            }

            return cellmix;
        }

        private int GetAlphaMapPosition(float position, float terrainPosition, int alphaMap)
        {
            var terrainData = _terrain.terrainData;

            return Mathf.RoundToInt((position - terrainPosition) / terrainData.size.x * alphaMap);
        }
    }
}