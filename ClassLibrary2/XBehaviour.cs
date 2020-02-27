using UnityEngine;
using EFT;
using System.Collections.Generic;
using EFT.Interactive;

namespace ClassLibrary2
{
    class XBehaviour : MonoBehaviourSingleton<XBehaviour>
    {

        private IEnumerable<LootItem> _i;
        private IEnumerable<Player> _p;

        private bool yy = false;
        private bool yx = false;
        private bool rr = false;
        private int zz = 0;
        private int angkfnaeg = 12;
        private bool keinHk = true;
        const string FICK_BATTLE_AUGE = "MACH KEIN AUGE BRUDI";
        //item_keycard
        private List<string> a = new List<string>() { "key", "docbag", "wallet", "transilluminator", "case", "bitcoin", "fuelconditioner", "video_card" };

        public void Start()
        {

        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                _i = FindObjectsOfType<LootItem>();
                yy = !yy;
            }

            if (Input.GetKeyDown(KeyCode.K))
            {
                _i = FindObjectsOfType<LootItem>();
                _p = FindObjectsOfType<Player>();
                yx = !yx;
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                _p = FindObjectsOfType<Player>();
                rr = !rr;
            }
        }

        public void OnGUI()
        {
            if (yy)
            {
                y();
            }

            if (yx)
            {
                ya();
            }

            if(rr)
            {
                dp();
            }

        } 

        private void y()
        {
            foreach (var Item in _i)
            {
                if (Item == null)
                    continue;

                foreach (var z1 in a)
                {
                    if (Item.name.Contains(z1) && !isTrash(Item))
                    {
                        float d = Vector3.Distance(Camera.main.transform.position, Item.transform.position);
                        int m = Mathf.RoundToInt(d);
                        Vector3 ItemBoundingVector = new Vector3(Camera.main.WorldToScreenPoint(Item.transform.position).x, Camera.main.WorldToScreenPoint(Item.transform.position).y, Camera.main.WorldToScreenPoint(Item.transform.position).z);
                        if (ItemBoundingVector.z > 0.01 && Item != null)
                        {
                            string t = Item.name + " - [" + m + "]m";
                            GUI.color = Color.red;
                            GUI.Label(new Rect(ItemBoundingVector.x - 50f, (float)Screen.height - ItemBoundingVector.y, 200f, 100f), t);
                            break;
                        }
                    }

                }
            }

            GUI.color = Color.cyan;
            GUI.Label(new Rect(40f, 200f, 800f, 800f), "Items");
        }

        private void ya()
        {
            string t = "Items:\n";

            foreach (var Item in _i)
            {
                if (Item == null)
                    continue;

                foreach (var z1 in a)
                {
                    if (Item.name.Contains(z1) && !isTrash(Item))
                    {
                        t += Item.name + "\n";
                    }
                }
            }


            t += "\n\nBosses:\n";

            foreach (var p in _p)
            {

                bool ai = p.Profile.Info.RegistrationDate <= 0;
                string pn = p.Profile.Info.Nickname;

                if (ai && p.Profile.Info.Settings.IsBoss())
                {
                    t += pn + "\n";
                }

            }

            GUI.color = Color.red;
            GUI.Label(new Rect(20f, 20f, 800f, 800f), t);
        }


        private void dp()
        {
            foreach (var p in _p)
            {
                float dto = Vector3.Distance(Camera.main.transform.position, p.Transform.position);
                int m = Mathf.RoundToInt(dto);
                Vector3 pbv = Camera.main.WorldToScreenPoint(p.Transform.position);
                Vector3 pbvH = Camera.main.WorldToScreenPoint(p.PlayerBones.Head.position);

                bool ai = p.Profile.Info.RegistrationDate <= 0;
                bool alive = p.HealthController.IsAlive;
                string pn = p.Profile.Info.Nickname;

                if (pbv.z > 0.01)
                {
                    if (ai && p.Profile.Info.Settings.IsBoss())
                    {
                        string t = ("BOSS - " + pn + " - [" + m + "]m");
                        GUI.color = Color.gray;
                        if (alive)
                        {
                            GUI.color = Color.magenta;
                            GUI.Box(new Rect(pbvH.x - 2.5f, (float)Screen.height - pbvH.y, 5f, 5f), "");
                        }
                        GUI.Label(new Rect(pbv.x - 50f, (float)Screen.height - pbv.y, 200f, 100f), t);
                    }
                    if (!ai && dto < 1000)
                    {
                        string t = (pn + " - [" + m + "]m");
                        GUI.color = Color.gray;
                        if (alive)
                        {
                            GUI.color = Color.green;
                            GUI.Box(new Rect(pbvH.x - 2.5f, (float)Screen.height - pbvH.y, 5f, 5f), "");

                            //Raycast
                            //Debug.DrawRay(pbvH, Vector3.forward, Color.green);
                        }
                        GUI.Label(new Rect(pbv.x - 50f, (float)Screen.height - pbv.y, 200f, 100f), t);
                    }
                    if(ai && alive && !p.Profile.Info.Settings.IsBoss() && dto < 250)
                    {
                        string t = p.Profile.Info.Settings.Role.ToString() +  " - [" + m + "]m";
                        GUI.color = Color.blue;
                        GUI.Label(new Rect(pbv.x - 50f, (float)Screen.height - pbv.y, 200f, 100f), t);

                        //Raycast
                        //Debug.DrawRay(p., p.PlayerBones.Head.position + new Vector3(0, 20, 0), Color.red);
                    }

                }

            }

            GUI.color = Color.cyan;
            GUI.Label(new Rect(120f, 200f, 800f, 800f), "Players");
        }

        private bool isTrash(LootItem Item)
        {
            return Item.name.Contains("keymod") || Item.name.Contains("flash_card");
        }

    }
}
