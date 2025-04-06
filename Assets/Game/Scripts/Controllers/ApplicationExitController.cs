using UnityEngine;
using Zenject;

namespace Game.Scripts.Controllers
{
    public class ApplicationExitController : ITickable
    {
        public void Tick()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
    }
}