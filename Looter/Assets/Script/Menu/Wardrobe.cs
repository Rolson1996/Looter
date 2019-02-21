using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Script.Menu
{
    class Wardrobe : MonoBehaviour
    {
        public List<Sprite> AllSkins = new List<Sprite>();
        public List<Sprite> UnlockedSkins = new List<Sprite>();
        public GameObject SelectedSkinIn;
        public GameObject SelectedSkinOut;
        private int MiddleSkin = 0;

        public GameObject WardrobeLeft;
        public GameObject WardrobeCentre;
        public GameObject WardrobeRight;


        public List<GameObject> CycleButtons = new List<GameObject>();

        void Start()
        {
            LoadUI();
        }

        public void LoadUI()
        {
            UnlockedSkins.Clear();

            foreach (KeyValuePair<int, bool> e in DataAndAchievementManager.instance.UnlockedSkins)
            {
                if (e.Value)
                {
                    UnlockedSkins.Add(AllSkins[e.Key]);
                }
               
            }


            if (UnlockedSkins.Count == 1)
            {
                WardrobeLeft.SetActive(false);
                WardrobeRight.SetActive(false);

                WardrobeCentre.transform.localPosition = new Vector3(0, 0, 0);
                WardrobeCentre.GetComponent<Image>().sprite = UnlockedSkins[0];

                foreach (GameObject go in CycleButtons)
                {
                    go.SetActive(false);
                }

                WardrobeCentre.GetComponent<Outline>().enabled = true;
            }
            if (UnlockedSkins.Count == 2)
            {
                WardrobeLeft.SetActive(false);
                WardrobeRight.SetActive(true);

                WardrobeCentre.transform.localPosition = new Vector3(-100, 0, 0);
                WardrobeCentre.GetComponent<Image>().sprite = UnlockedSkins[0];

                WardrobeRight.transform.localPosition = new Vector3(100, 0, 0);
                WardrobeRight.GetComponent<Image>().sprite = UnlockedSkins[1];

                foreach (GameObject go in CycleButtons)
                {
                    go.SetActive(false);
                }
            }
            else
            {
                WardrobeLeft.SetActive(true);
                WardrobeRight.SetActive(true);

                WardrobeLeft.transform.localPosition = new Vector3(-150, 0, 0);
                WardrobeLeft.GetComponent<Image>().sprite = UnlockedSkins[UnlockedSkins.Count - 1];

                WardrobeCentre.transform.localPosition = new Vector3(0, 0, 0);
                WardrobeCentre.GetComponent<Image>().sprite = UnlockedSkins[0];

                WardrobeRight.transform.localPosition = new Vector3(150, 0, 0);
                WardrobeRight.GetComponent<Image>().sprite = UnlockedSkins[1];

                foreach (GameObject go in CycleButtons)
                {
                    go.SetActive(true);
                }
            }

            DataAndAchievementManager.instance.currentSkin = UnlockedSkins[DataAndAchievementManager.instance.currentSkinNumber];
            SelectedSkinIn.GetComponent<Image>().sprite = UnlockedSkins[DataAndAchievementManager.instance.currentSkinNumber];
            SelectedSkinOut.GetComponent<Image>().sprite = UnlockedSkins[DataAndAchievementManager.instance.currentSkinNumber];
            SelectedSkinOut.GetComponent<RectTransform>().rotation = new Quaternion(0,0,180,0);
        }

        public void CycleSkins(int direction)
        {
            MiddleSkin = MiddleSkin + direction;

            if (MiddleSkin >= UnlockedSkins.Count)
            {
                MiddleSkin = 0;
            }
            else if (MiddleSkin < 0)
            {
                MiddleSkin = UnlockedSkins.Count - 1;
            }

            WardrobeCentre.GetComponent<Image>().sprite = UnlockedSkins[MiddleSkin];


            if (MiddleSkin == 0)
            {
                WardrobeLeft.GetComponent<Image>().sprite = UnlockedSkins[UnlockedSkins.Count - 1];
                WardrobeRight.GetComponent<Image>().sprite = UnlockedSkins[1];


                if (DataAndAchievementManager.instance.currentSkinNumber == 0)
                {
                    WardrobeCentre.GetComponent<Outline>().enabled = true;
                    WardrobeLeft.GetComponent<Outline>().enabled = false;
                    WardrobeRight.GetComponent<Outline>().enabled = false;
                }
                else if (DataAndAchievementManager.instance.currentSkinNumber == 1)
                {
                    WardrobeCentre.GetComponent<Outline>().enabled = false;
                    WardrobeLeft.GetComponent<Outline>().enabled = false;
                    WardrobeRight.GetComponent<Outline>().enabled = true;
                }
                else if (DataAndAchievementManager.instance.currentSkinNumber == UnlockedSkins.Count - 1)
                {
                    WardrobeCentre.GetComponent<Outline>().enabled = false;
                    WardrobeLeft.GetComponent<Outline>().enabled = true;
                    WardrobeRight.GetComponent<Outline>().enabled = false;
                }
                else
                {
                    WardrobeCentre.GetComponent<Outline>().enabled = false;
                    WardrobeLeft.GetComponent<Outline>().enabled = false;
                    WardrobeRight.GetComponent<Outline>().enabled = false;
                }

            }
            else if (MiddleSkin == UnlockedSkins.Count - 1)
            {
                WardrobeRight.GetComponent<Image>().sprite = UnlockedSkins[0];
                WardrobeLeft.GetComponent<Image>().sprite = UnlockedSkins[UnlockedSkins.Count - 2];

                if (DataAndAchievementManager.instance.currentSkinNumber == UnlockedSkins.Count - 1)
                {
                    WardrobeCentre.GetComponent<Outline>().enabled = true;
                    WardrobeLeft.GetComponent<Outline>().enabled = false;
                    WardrobeRight.GetComponent<Outline>().enabled = false;
                }
                else if (DataAndAchievementManager.instance.currentSkinNumber == 0)
                {
                    WardrobeCentre.GetComponent<Outline>().enabled = false;
                    WardrobeLeft.GetComponent<Outline>().enabled = false;
                    WardrobeRight.GetComponent<Outline>().enabled = true;
                }
                else if (DataAndAchievementManager.instance.currentSkinNumber == UnlockedSkins.Count - 2)
                {
                    WardrobeCentre.GetComponent<Outline>().enabled = false;
                    WardrobeLeft.GetComponent<Outline>().enabled = true;
                    WardrobeRight.GetComponent<Outline>().enabled = false;
                }
                else
                {
                    WardrobeCentre.GetComponent<Outline>().enabled = false;
                    WardrobeLeft.GetComponent<Outline>().enabled = false;
                    WardrobeRight.GetComponent<Outline>().enabled = false;
                }
            }
            else
            {
                WardrobeLeft.GetComponent<Image>().sprite = UnlockedSkins[MiddleSkin - 1];
                WardrobeRight.GetComponent<Image>().sprite = UnlockedSkins[MiddleSkin + 1];


                if (DataAndAchievementManager.instance.currentSkinNumber == MiddleSkin)
                {
                    WardrobeCentre.GetComponent<Outline>().enabled = true;
                    WardrobeLeft.GetComponent<Outline>().enabled = false;
                    WardrobeRight.GetComponent<Outline>().enabled = false;
                }
                else if (DataAndAchievementManager.instance.currentSkinNumber == MiddleSkin + 1)
                {
                    WardrobeCentre.GetComponent<Outline>().enabled = false;
                    WardrobeLeft.GetComponent<Outline>().enabled = false;
                    WardrobeRight.GetComponent<Outline>().enabled = true;
                }
                else if (DataAndAchievementManager.instance.currentSkinNumber == MiddleSkin -1)
                {
                    WardrobeCentre.GetComponent<Outline>().enabled = false;
                    WardrobeLeft.GetComponent<Outline>().enabled = true;
                    WardrobeRight.GetComponent<Outline>().enabled = false;
                }
                else
                {
                    WardrobeCentre.GetComponent<Outline>().enabled = false;
                    WardrobeLeft.GetComponent<Outline>().enabled = false;
                    WardrobeRight.GetComponent<Outline>().enabled = false;
                }
            }

            
        }

        public void SelectSkin(int BoxNumber)
        {
            int SelectedSkinNumber = MiddleSkin + BoxNumber;

            if (SelectedSkinNumber >= UnlockedSkins.Count)
            {
                SelectedSkinNumber = 0;
            }
            else if (SelectedSkinNumber < 0)
            {
                SelectedSkinNumber = UnlockedSkins.Count - 1;
            }

            DataAndAchievementManager.instance.currentSkinNumber = SelectedSkinNumber;
            DataAndAchievementManager.instance.currentSkin = UnlockedSkins[SelectedSkinNumber];

            SelectedSkinIn.GetComponent<Image>().sprite = UnlockedSkins[SelectedSkinNumber];
            SelectedSkinOut.GetComponent<Image>().sprite = UnlockedSkins[SelectedSkinNumber];


            if(BoxNumber == 0)
            {
                WardrobeCentre.GetComponent<Outline>().enabled = true;
                WardrobeLeft.GetComponent<Outline>().enabled = false;
                WardrobeRight.GetComponent<Outline>().enabled = false;
            }
            else if (BoxNumber == 1)
            {
                WardrobeCentre.GetComponent<Outline>().enabled = false;
                WardrobeLeft.GetComponent<Outline>().enabled = false;
                WardrobeRight.GetComponent<Outline>().enabled = true;
            }
            else if (BoxNumber == -1)
            {
                WardrobeCentre.GetComponent<Outline>().enabled = false;
                WardrobeLeft.GetComponent<Outline>().enabled = true;
                WardrobeRight.GetComponent<Outline>().enabled = false;
            }
        }
    }
}
