import { Component, OnInit, Input } 
from '@angular/core';

export class Map {
    canvas;
    context;
    width:number;
    height:number;

    spaceShipSprite;
    x:number = 0;
    y:number = 0;
    xoff:number = 0;
    yoff:number = 0;

    canvasRect : ClientRect;
    mouseX:number;
    mouseY:number;

    public Init():void
    {
        this.canvas = document
            .getElementById('map');
        this.context = this.canvas.getContext('2d');
        this.width = this.canvas.width; 
        this.height = this.canvas.height;

        this.canvasRect = document
                .getElementById('map')
                .getBoundingClientRect();

        //console.log("" + this.canvasRect.bottom);
        this.canvas
            .addEventListener(
                'mousemove', function(event) {
                    let rect: ClientRect = 
                            document
                            .getElementById('map')
                            .getBoundingClientRect();
                    this.mouseX = 
                        event.clientX - rect.left;
                    this.mouseY = 
                        event.clientY - rect.top;
                    //console.log("x: " + this.mouseX + "y: " + this.mouseY);
                }, false);

        //нужно качать изображение с сервера
        //this.spaceShipSprite = new Image();
        //this.spaceShipSprite.src = "spaceship1.png";
        //this.spaceShipSprite.onload = 
        //    function(e) {this.context.drawImage(this.spaceShipSprite, 0, 0);;};
    }

    public Clear():void
    {
        this.context.clearRect(0, 0, 
            this.width, this.height);
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
        //console.log("Draw");

        this.context.fillStyle = "red";
        this.DrawCircle(
            (this.canvasRect.right - this.canvasRect.left) / 2,
            (this.canvasRect.bottom - this.canvasRect.top) / 2,
             50, "yellow");

        if (this.x > 100)
        {
            this.xoff = -0.1;
        }
        else {
            this.xoff = +0.1;
        }

        
        this.context.fillRect(0, 0, 100+this.x, 100);
        this.x += this.xoff;

        window.requestAnimationFrame(this.DrawWorld);
    }


}
