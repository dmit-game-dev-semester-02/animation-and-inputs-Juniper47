﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CelAnimation;
    
/// <summary>
/// Controls playback of a CelAnimationSequence.
/// Note: a CelAnimationPlayer can only play ONE animation at a time.   
/// </summary>
public class CelAnimationPlayerMultiRow
{
    private CelAnimationSequenceMultiRow celAnimationSequence;
    private int celIndex;
    private float celTimeElapsed;
    public Rectangle celSourceRectangle;

    /// <summary>
    /// Begins or continues playback of a CelAnimationSequence.
    /// </summary>
    public void Play(CelAnimationSequenceMultiRow celAnimationSequence)
    {
        if (celAnimationSequence == null)
        {
            throw new Exception("CelAnimationPlayer.PlayAnimation received null CelAnimationSequence");
        }
        // If this animation is already running, do not restart it...
        if (celAnimationSequence != this.celAnimationSequence)
        {
            this.celAnimationSequence = celAnimationSequence;
            //SITUATION ONE: MANY ROWS, ONE ANIMATION
            //cellIndexColumn
            //cellIndexRow
            celIndex = 0;
            celTimeElapsed = 0.0f;

            celSourceRectangle.X = 0;
            celSourceRectangle.Y = 0;
            celSourceRectangle.Width = this.celAnimationSequence.CelWidth;
            celSourceRectangle.Height = this.celAnimationSequence.CelHeight;
        }
    }

    /// <summary>
    /// Update the state of the CelAnimationPlayer.
    /// </summary>
    /// <param name="gameTime">Provides a snapshot of timing values.</param>
    public void Update(GameTime gameTime)
    {
        if (celAnimationSequence != null)
        {
            celTimeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (celTimeElapsed >= celAnimationSequence.CelTime)
            {
                celTimeElapsed -= celAnimationSequence.CelTime;

                // Advance the frame index looping as appropriate...
                //SITUATION ONE: MANY ROWS, ONE ANIMATION
                //celIndexColumn = (celIndex + 1) % celAnimationSequence.CelCount;
                //celIndexRow = (celIndex + 1) % celAnimationSequence.CelCount;
                celIndex = (celIndex + 1) % celAnimationSequence.CelCount;

                celSourceRectangle.X = celIndex * celSourceRectangle.Width;
            }
        }
    }

    /// <summary>
    /// Draws the current cel of the animation.
    /// </summary>
    public void Draw(SpriteBatch spriteBatch, Vector2 position, SpriteEffects spriteEffects)
    {
        if (celAnimationSequence != null)
        {
            spriteBatch.Draw(celAnimationSequence.Texture, position, celSourceRectangle, Color.White, 0.0f, Vector2.Zero, 1.0f, spriteEffects, 0.0f);
        }
    }
}

