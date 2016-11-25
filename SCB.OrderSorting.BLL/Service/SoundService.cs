using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace SCB.OrderSorting.BLL.Service
{
    public  class SoundService
    {
       
        public  void playSound(string SoundPath)
        {
            SoundPlayer simpleSound = new SoundPlayer(SoundPath);
            simpleSound.Play();
        }
        public  Task playSoundAsny(string SoundPath)
        {
           return Task.Run(()=> playSound(SoundPath));
        }
    }
    
}
