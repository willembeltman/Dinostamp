window.gameInterop = {
    dotnet: null,
    canvas: null,
    ctx: null,

    init(dotnetInstance) {
        this.dotnet = dotnetInstance;
        this.canvas = document.getElementById('game');
        this.ctx = this.canvas.getContext('2d');

        // Controls
        window.addEventListener('keydown', this.keyDown);
        window.addEventListener('keyup', this.keyUp);

        // Start je game loop
        requestAnimationFrame(this.gameLoop.bind(this));
    },

    async keyDown(e) {
        await window.gameInterop.dotnet.invokeMethodAsync("KeyDown", e.key);
    },

    async keyUp(e) {
        await window.gameInterop.dotnet.invokeMethodAsync("KeyUp", e.key);
    },

    async gameLoop() {
        let width = this.canvas.width;
        let height = this.canvas.height;

        // Haal frame uit C#
        const frame = await this.dotnet.invokeMethodAsync("GetFrame", width, height);

        // Render op canvas via JS
        render(this.ctx, width, height, frame);

        // Volgende frame
        requestAnimationFrame(this.gameLoop.bind(this));
    }
}


function render(ctx, width, height, frame) {
    ctx.save();
    // Inbouwen dat emenies wankelen van jou stamp
    if (frame.camera.cameraShake > 0) {
        ctx.translate(Math.random() * frame.camera.cameraShake - frame.camera.cameraShake / 2, Math.random() * frame.camera.cameraShake - frame.camera.cameraShake / 2);
    }
    if (frame.soundEffects.boom) {
        boom.currentTime = 0;
        //boom.play();
    }
    ctx.clearRect(0, 0, width, height);
    drawPlatforms(ctx, frame, width, height);
    drawEnemies(ctx, frame, width, height);
    drawPlayer(ctx, frame, width, height);
    ctx.restore();
    //drawUI(ctx, frame, width, height);
}

function drawPlatforms(ctx, frame, width, height) {
    for (const p of frame.level.platforms) {
        ctx.save();
        ctx.globalAlpha = 1;
        ctx.fillStyle = '#964B00';
        ctx.fillRect(p.x - frame.camera.x, p.y, p.width, p.height);
        ctx.restore();
    }
}
function drawEnemies(ctx, frame, width, height) {
    for (const e of frame.level.enemies) {
        ctx.save();
        ctx.fillStyle = e.color;
        ctx.fillRect(e.x - frame.camera.x, e.y, e.width, e.height);
        ctx.restore();
    }
}
function drawPlayer(ctx, frame, width, height) {
    // Simpel T-Rex sprite
    ctx.save();
    ctx.translate(frame.player.x - frame.camera.x, frame.player.y);
    ctx.scale(frame.player.facing, 1);
    ctx.fillStyle = frame.player.Color;
    ctx.fillRect(-frame.player.width / 2, -frame.player.height, frame.player.width, frame.player.height);
    // Staart
    ctx.fillStyle = frame.player.Color;
    ctx.fillRect(-frame.player.width, -frame.player.height / 4, frame.player.width / 2, frame.player.height / 4);
    ctx.fillStyle = '#384';
    ctx.fillRect(-frame.player.w / 4 * 3, -frame.player.height / 2, frame.player.width / 4, frame.player.height / 4);
    // Oog
    ctx.fillStyle = '#fff';
    ctx.beginPath();
    ctx.arc(18, -frame.player.height + 18, 8, 0, Math.PI * 2);
    ctx.fill();
    ctx.fillStyle = '#222';
    ctx.beginPath();
    ctx.arc(21, -frame.player.height + 18, 3, 0, Math.PI * 2);
    ctx.fill();
    ctx.restore();
}
function drawUI(ctx, frame, width, height) {
    ctx.save();
    ctx.font = 'bold 12px sans-serif';
    ctx.fillStyle = '#fff';
    ctx.fillText('DinoStamp: T-Rex Platformer' + JSON.stringify(frame.player), 24, 20);
    ctx.fillText(JSON.stringify(frame.player), 24, 40);
    ctx.fillText(JSON.stringify(frame.level.enemies), 24, 60);
    ctx.restore();
}