using System;
using UnityEngine;
using UnityEditor;

namespace LOK1game.Editor
{
    public enum EWeaponBaseScript
    {
        Gun,
        Drill,
        GroundCrystal,
    }

    [Obsolete]
    public class EditorCreateWeaponWindow : BaseLOK1gameEditorWindow
    {
        private string _weaponName = "new Weapon";
        private Sprite _weaponIcon;
        private GameObject _weaponModel;
        //private EWeaponType _weaponType = EWeaponType.Primary;
        private EWeaponBaseScript _weaponBaseScript;
        private bool _registerWeaponToWeaponManager;

        [MenuItem(MENU_ITEM_NAME+"/CHD/Create new weapon")]
        public static void ShowWindow()
        {
            GetWindow<EditorCreateWeaponWindow>("Create weapon");
        }

        private void OnGUI()
        {
            GUILayout.Space(20f);

            _weaponName = EditorGUILayout.TextField("Weapon name: ", _weaponName);
            _weaponIcon = (Sprite)EditorGUILayout.ObjectField("Weapon icon: ", _weaponIcon, typeof(Sprite));
            _weaponModel = (GameObject)EditorGUILayout.ObjectField("Weapon model: ", _weaponModel, typeof(GameObject));
            //_weaponType = (EWeaponType)EditorGUILayout.EnumPopup("Weapon type: ", _weaponType);
            _weaponBaseScript = (EWeaponBaseScript)EditorGUILayout.EnumPopup("Weapon base script: ", _weaponBaseScript);
            _registerWeaponToWeaponManager = EditorGUILayout.Toggle("Register weapon to app weapon manager: ", _registerWeaponToWeaponManager);

            if(GUILayout.Button("Create weapon"))
            {
                var parent = new GameObject(_weaponName);
                //var weapon = GetBaseWeaponClass(_weaponBaseScript, parent);
                Instantiate(_weaponModel, parent.transform);

                //var data = CreateInstance<WeaponData>();
                //data.name = _weaponName;

                //weapon.Editor_SetData(data);

                if(_registerWeaponToWeaponManager)
                {
                    //App.ProjectContext.WeaponManager.Editor_AddWeapon(data);
                    //WeaponLibrary.Editor_AddWeapon(data);
                }

                //AssetDatabase.CreateAsset(data, GetCurrentPath(Constants.Editor.WEAPON_DATA_PATH, false));
                //AssetDatabase.CreateFolder(Constants.Editor.WEAPON_PREFAB_PATH, _weaponName);
                //AssetDatabase.CreateFolder($"{Constants.Editor.WEAPON_PREFAB_PATH}/{_weaponName}", "Prefabs");
                //PrefabUtility.SaveAsPrefabAsset(parent, GetCurrentPath($"{Constants.Editor.WEAPON_PREFAB_PATH}/{_weaponName}/Prefabs", true));

                //Additional folders
                var animsPath = "Assets/_Game/Character/Player/Arms/Animations";
                AssetDatabase.CreateFolder(animsPath, _weaponName);

                var controller = new AnimatorOverrideController
                {
                    runtimeAnimatorController = AssetDatabase.LoadAssetAtPath<RuntimeAnimatorController>("Assets/_Game/Character/Player/Arms/PlayerArmsAnimController.controller")
                };
                AssetDatabase.CreateAsset(controller, $"{animsPath}/{_weaponName}/Arms{_weaponName}.overrideController");

                //data.Editor_SetData(_weaponType, weapon, controller);

                DestroyImmediate(parent);

                //data.Editor_FindAndSetPrefab();
            }

            DrawLogo(170f);
        }

        private string GetCurrentPath(string path, bool prefab)
        {
            if (prefab)
            {
                return $"{path}/{_weaponName}.prefab";
            }
            else
            {
                return $"{path}/{_weaponName}.asset";
            }
        }

        //private BaseWeapon GetBaseWeaponClass(EWeaponBaseScript baseScript, GameObject parent)
        //{
        //    switch (baseScript)
        //    {
        //        case EWeaponBaseScript.Gun:
        //            return parent.AddComponent<Gun>();
        //        case EWeaponBaseScript.Drill:
        //            return parent.AddComponent<PlayerDrillWeapon>();
        //        case EWeaponBaseScript.GroundCrystal:
        //            return parent.AddComponent<GroundCrystalWeapon>();
        //        default:
        //            return parent.AddComponent<Gun>();
        //    }
        //}
    }
}