import { Component, OnInit, Input } from '@angular/core';

export class Map {
    canvas;
    context;
    width:number;
    height:number;

    x:number;

    public Init():void
    {
        this.canvas = document
            .getElementById('map');
        this.context = this.canvas.getContext('2d');
        this.width = this.canvas.width; 
        this.height = this.canvas.height; 
    }

    Clear():void
    {
        this.context.clearRect(0, 0, this.width, this.height);
    }

    DrawCircle(x:number,y:number,radius:number,color:string):void
    {
        this.context.save();
        
        if (color)
        {
            this.context.fillStyle = color;
        }
        this.context.beginPath();
        this.context.arc(x,y,radius, 0, 2*Math.PI, true);
        this.context.closePath();
        this.context.fill();

        this.context.restore();
    }

    public DrawWorld():void
    {
        this.Clear();
        
        this.context.fillStyle = "red";
        this.DrawCircle(200,200, 50, "yellow");
        this.context.fillRect(0, 0, 100, 100);

        window.requestAnimationFrame(this.DrawWorld);
    }


}
