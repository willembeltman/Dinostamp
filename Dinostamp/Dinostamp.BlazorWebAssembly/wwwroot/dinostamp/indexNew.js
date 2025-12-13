
const canvas = document.getElementById('game');
const ctx = canvas.getContext('2d');
const W = canvas.width
const H = canvas.height;

// Game world
const gravity = 1.1;
const groundY = H - 80;
let cameraX = 0;
let cameraShake = 0;
let shakeTime = 0;

const groundplatforms = [
    {x: 0, y: groundY, w: 1000, h: groundY},
    {x: 2500, y: groundY, w: 1000, h: groundY},
    {x: 5000, y: groundY - 20, w: 1000, h: groundY},
    {x: 7000, y: groundY + 20, w: 1000, h: groundY},
    {x: 12000, y: groundY, w: 1000, h: groundY},
];

const normalplatforms = [
    {x: 500, y: groundY - 120, w: 120, h: 50},
    {x: 800, y: groundY - 200, w: 100, h: 50},
    {x: 1100, y: groundY - 120, w: 120, h: 50},
    {x: 1400, y: groundY - 200, w: 100, h: 50},
    {x: 1700, y: groundY - 120, w: 80, h: 50},
    {x: 2000, y: groundY - 160, w: 120, h: 50},
    {x: 2300, y: groundY - 100, w: 100, h: 50},

    // Begin extra sectie
    {x: 2600, y: groundY - 140, w: 120, h: 50},
    {x: 2900, y: groundY - 180, w: 100, h: 50},
    {x: 3200, y: groundY - 120, w: 80, h: 50},
    {x: 3500, y: groundY - 160, w: 100, h: 50},
    {x: 3800, y: groundY - 100, w: 120, h: 50},
    {x: 4100, y: groundY - 200, w: 80, h: 50},
    {x: 4400, y: groundY - 150, w: 100, h: 50},
    {x: 4700, y: groundY - 100, w: 120, h: 50},
    {x: 5000, y: groundY - 180, w: 100, h: 50},
    {x: 5300, y: groundY - 120, w: 80, h: 50},
    {x: 5600, y: groundY - 160, w: 100, h: 50},
    {x: 5900, y: groundY - 100, w: 120, h: 50},
    {x: 6200, y: groundY - 200, w: 80, h: 50},
    {x: 6500, y: groundY - 140, w: 100, h: 50},
    {x: 6800, y: groundY - 120, w: 120, h: 50},
    {x: 7100, y: groundY - 180, w: 100, h: 50},
    {x: 7400, y: groundY - 150, w: 80, h: 50},
    {x: 7700, y: groundY - 100, w: 100, h: 50},
    {x: 8000, y: groundY - 200, w: 120, h: 50},
    {x: 8300, y: groundY - 160, w: 100, h: 50},
    {x: 8600, y: groundY - 120, w: 80, h: 50},
    {x: 8900, y: groundY - 180, w: 100, h: 50},
    {x: 9200, y: groundY - 140, w: 120, h: 50},
    {x: 9500, y: groundY - 100, w: 100, h: 50},
    {x: 9800, y: groundY - 160, w: 80, h: 50},
    {x: 10100, y: groundY - 200, w: 100, h: 50},
    {x: 10400, y: groundY - 120, w: 120, h: 50},
    {x: 10700, y: groundY - 150, w: 100, h: 50},
    {x: 11000, y: groundY - 100, w: 80, h: 50},
    {x: 11300, y: groundY - 180, w: 100, h: 50},
    {x: 11600, y: groundY - 140, w: 120, h: 50},
];

const platforms = [...groundplatforms, ...normalplatforms];

const player = {
    x: 100,
    y: groundY - 80,
    vx: 0,
    vy: 0,
    w: 64,
    h: 80,
    speed: 6,
    jumping: false,
    starred: false,
    facing: 1,
    color: '#6c3',
};

const enemies = [ 
    {
        x: groundplatforms[0].x + 200,
        y: groundplatforms[0].y - 40,
        w: 40,
        h: 40,
        vx: 2,
        dir: 1,
        color: '#c33',
        platform: groundplatforms[0],
        poisonous: false
    }
];

const keys = {};
window.addEventListener('keydown', e => keys[e.key.toLowerCase()] = true);
window.addEventListener('keyup', e => keys[e.key.toLowerCase()] = false);

// Audio
const boom = document.getElementById('boom');

function updateEnemies() {
    for (const e of enemies) {
        e.x += e.vx * e.dir;
        // Keer om aan de randen van het platform
        if (e.x < e.platform.x) {
            e.x = e.platform.x;
            e.dir = 1;
        } else if (e.x + e.w > e.platform.x + e.platform.w) {
            e.x = e.platform.x + e.platform.w - e.w;
            e.dir = -1;
        }
    }
}

// Collision tussen speler en vijand
function checkEnemyCollision() {
    for (const e of enemies) {
        // AABB collision
        if (
            player.x + player.w / 2 > e.x &&
            player.x - player.w / 2 < e.x + e.w &&
            player.y + player.h / 2 > e.y &&
            player.y - player.h / 2 < e.y + e.h
        ) {
            player.inCollision = true;
            // Enemy is "sterker" tenzij hij giftig is
            if (!e.poisonous) {
                // Duw speler weg afhankelijk van enemy richting
                const push = 12;
                if (player.x < e.x) {
                    player.x -= push;
                } else {
                    player.x += push;
                }
                // Eventueel shake/geluid
                shakeScreen();
            } else {
                // Hier kun je giftig effect toevoegen
            }
        }
        else {
            
            player.inCollision = false;
        }
    }
}

function drawEnemies() {
    for (const e of enemies) {
        ctx.save();
        ctx.fillStyle = e.color;
        ctx.fillRect(e.x - cameraX, e.y, e.w, e.h);
        ctx.restore();
    }
}


function playBoom() {

    boom.currentTime = 0;
    //boom.play();
}

function shakeScreen() {
    cameraShake = 24;
    shakeTime = 12;
}

function drawPlayer() {
    // Simpel T-Rex sprite
    ctx.save();
    ctx.translate(player.x - cameraX, player.y);
    ctx.scale(player.facing, 1);
    ctx.fillStyle = player.color;
    ctx.fillRect(-player.w/2, -player.h, player.w, player.h);
    // Staart
    ctx.fillStyle = player.color;
    ctx.fillRect(-player.w, -player.h/4, player.w/2, player.h/4);
    ctx.fillStyle = '#384';
    ctx.fillRect(-player.w/4*3, -player.h/2, player.w/4, player.h/4);
    // Oog
    ctx.fillStyle = '#fff';
    ctx.beginPath();
    ctx.arc(18, -player.h+18, 8, 0, Math.PI*2);
    ctx.fill();
    ctx.fillStyle = '#222';
    ctx.beginPath();
    ctx.arc(21, -player.h+18, 3, 0, Math.PI*2);
    ctx.fill();
    ctx.restore();
}

function drawPlatforms() {
    for (const p of platforms) {
        ctx.save();
        ctx.globalAlpha = 1;
        ctx.fillStyle = '#964B00';
        ctx.fillRect(p.x - cameraX, p.y, p.w, p.h);
        ctx.restore();
    }
}

function drawUI() {
    ctx.save();
    ctx.font = 'bold 12px sans-serif';
    ctx.fillStyle = '#fff';
    ctx.fillText('DinoStamp: T-Rex Platformer' + JSON.stringify(player), 24, 20);
    ctx.fillText(JSON.stringify(player), 24, 40);
    ctx.fillText(JSON.stringify(enemies), 24, 60);
    ctx.restore();
}

function updatePlayer() {
    if (player.y > H) {
        player.x = 100;
        player.y = groundY - 80;
        player.vy = 0;
    }

    // Horizontaal
    if (keys['a'] || keys['arrowleft']) {
        player.vx = -player.speed;
        player.facing = -1;
    } else if (keys['d'] || keys['arrowright']) {
        player.vx = player.speed;
        player.facing = 1;
    } else {
        player.vx = 0;
    }
    // Jump
    if ((keys[' '] || keys['z']|| keys['arrowup']) && !player.jumping) {
        player.vy = -22;
        player.jumping = true;
    }
    if (keys['p']){
        player.starred = !player.starred;
    }

    // Apply
    player.x += player.vx;
    player.y += player.vy;
    player.vy += gravity;

    // Collision (AABB, verbeterd)
    let onGround = false;
    for (const p of platforms) {
        if (
            player.x + player.w/2 > p.x &&
            player.x - player.w/2 < p.x + p.w &&
            player.y + 2 > p.y &&
            player.y + 2 < p.y + p.h &&
            player.vy >= 0
        ) {
            if (player.starred) {
                // Blijf springen
                player.y = p.y - player.h;
            } else {
                player.y = p.y;
            }
            player.vy = 0;
            onGround = true;
        }
    }
    if (onGround) {
        if (player.jumping) {
            playBoom();
            shakeScreen();
        }
        player.jumping = false;
    } else {
        player.jumping = true;
    }

    // Camera volgen
    cameraX += ((player.x - 300) - cameraX) * 0.1;
    if (cameraX < 0) cameraX = 0;
}

function updateShake() {
    if (shakeTime > 0) {
        shakeTime--;
        cameraShake *= 0.85;
        if (cameraShake < 1) cameraShake = 0;
    } else {
        cameraShake = 0;
    }
}

function render() {
    ctx.save();
    if (cameraShake > 0) {
        ctx.translate(Math.random()*cameraShake - cameraShake/2, Math.random()*cameraShake - cameraShake/2);
    }
    ctx.clearRect(0, 0, W, H);
    drawPlatforms();
    drawEnemies();
    drawPlayer();
    ctx.restore();
    drawUI();
}

function gameLoop() {
    updatePlayer();
    updateEnemies();
    checkEnemyCollision();
    updateShake();
    render();
    requestAnimationFrame(gameLoop);
}

function init(){

    gameLoop();
}

init();