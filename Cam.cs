using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using System;


namespace AgeOfWar
{
    class Cam
    {
        public Matrix transfom { get; private set; }

        public void follow(Player target)
        {
            var pos = Matrix.CreateTranslation(-target.playerPos.X - (target.playerRect.Width / 2), 0, 0);
            var offset = Matrix.CreateTranslation(860, 350, 0);

            transfom = pos * offset;
        }

    }
}
