using Giny.Core.Network;
using Giny.IO;
using Giny.IO.D2P;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.WorldEditor.Caching
{
    public class ExternalResources
    {
        static D2PFile ItemIconsFile;

        public static void Initialize()
        {
            ItemIconsFile = new D2PFile(Path.Combine(ClientConstants.ClientPath, ClientConstants.ItemBitmap0Path));
        }

        public static async Task<string> GetSpellIcon(int iconId)
        {
            string url = string.Format("https://static.ankama.com/dofus/www/game/spells/55/{0}.png", iconId);
            return await GetBase64FromUrlAsync(url);

        }

        public static async Task<string> GetEntityLookIcon(string look, int size)
        {
            byte[] bytes = Encoding.Default.GetBytes(look);
            var hexString = BitConverter.ToString(bytes);
            hexString = hexString.Replace("-", "").ToLower();

            // 
            string url = string.Format("https://www.dofusbook.net/flash/{0}/full/1/{1}_{1}.png", hexString, size);
            return await GetBase64FromUrlAsync(url);
        }
        public static string GetItemIcon(int iconId)
        {
            var icon = ItemIconsFile.Entries.FirstOrDefault(x => Path.GetFileNameWithoutExtension(x.FileName) == iconId.ToString());
            var entryData = ItemIconsFile.ReadFile(icon);
            string base64String = Convert.ToBase64String(entryData);
            return base64String;
        }

        static async Task<string> GetBase64FromUrlAsync(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    byte[] bytes = await response.Content.ReadAsByteArrayAsync();
                    string base64String = Convert.ToBase64String(bytes);
                    return base64String;
                }
                else
                {
                    throw new Exception($"Failed to download image. Status code: {response.StatusCode}");
                }
            }
        }

    }
}
