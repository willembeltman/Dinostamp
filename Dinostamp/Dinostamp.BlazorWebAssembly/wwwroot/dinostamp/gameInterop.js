window.gameInterop = {
    dotnet: null,
    canvas: null,
    ctx: null,
    W: 0,
    H: 0,

    init(dotnetInstance) {
        this.dotnet = dotnetInstance;

        // Start je game loop
        requestAnimationFrame(this.gameLoop.bind(this));
    },

    async gameLoop() {
        // Voorbeeld: inputs poll’en
        const inputs = {
            left: keyLeftPressed,
            right: keyRightPressed,
            jump: keyJumpPressed
        };

        // Stuur inputs naar C#
        await this.dotnet.invokeMethodAsync("UpdateInputs", inputs);

        // Haal frame uit C#
        const frame = await this.dotnet.invokeMethodAsync("GetFrame");

        // Render op canvas via JS
        render(frame);

        requestAnimationFrame(this.gameLoop.bind(this));
    }
}

const canvas = document.getElementById('game');
const ctx = canvas.getContext('2d');
const W = canvas.width
const H = canvas.height;

function render(frame) {
    ctx.save();
    if (cameraShake > 0) {
        ctx.translate(Math.random() * cameraShake - cameraShake / 2, Math.random() * cameraShake - cameraShake / 2);
    }
    ctx.clearRect(0, 0, W, H);
    drawPlatforms();
    drawEnemies();
    drawPlayer();
    ctx.restore();
    drawUI();
}