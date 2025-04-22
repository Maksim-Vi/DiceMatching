using Code.Board;
using CustomInput;
using UnityEngine;
using Zenject;

// Project installer for services what need to use per game LvlService, MoneyService, AudioService etc.
namespace Matching.Core
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {        
            if(SystemInfo.deviceType == DeviceType.Handheld){
                Container.BindInterfacesAndSelfTo<MobileInputDevice>().AsSingle().NonLazy();
            } else {
                Container.BindInterfacesAndSelfTo<DesktopInputDevice>().AsSingle().NonLazy();
            }

            Container.Bind<InputHandler>().AsSingle().NonLazy();
            Container.Bind<SwipeHandler>().AsSingle().NonLazy();
            
            Container.BindInterfacesAndSelfTo<LocalResoursesManager>().AsSingle().NonLazy();

            Debug.Log("ProjectInstaller initialized");
        }
    }
}