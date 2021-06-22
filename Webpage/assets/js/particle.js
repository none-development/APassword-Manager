var camera, scene, renderer, particles, particle, material, particleCount, points, texture;
var xSpeed, ySpeed;
xSpeed = 0.0005;
ySpeed = 0.001;
var winWidth, winHeight;
winWidth = window.innerWidth;
winHeight = window.innerHeight;
window.addEventListener( 'resize', onWindowResize, false );


init();
animate();

function init(){
  const texture2 = new THREE.TextureLoader().load( "./js/circle.png" );
  scene = new THREE.Scene();
  scene.fog = new THREE.FogExp2(0xFF0000, 0.0009);

  camera = new THREE.PerspectiveCamera(75, winWidth/winHeight, 1,1500);
  camera.position.x = 50;


  material = new THREE.PointsMaterial({
    map: texture2,
    color: 0x4287f5,
    size: 2,
    transparent: true,
    blending: THREE.AdditiveBlending
  });

  particleCount = 15000;
  particles = new THREE.Geometry();

  for (var i = 0; i < particleCount; i++) {
    var px = Math.random() * 2000 - 1000;
    var py = Math.random() * 2000 - 1000;
    var pz = Math.random() * 2000 - 1000;
    particle = new THREE.Vector3(px, py, pz);
    particle.velocity = new THREE.Vector3(0, Math.random(), 0);
    particles.vertices.push(particle);
  }

  points = new THREE.Points(particles, material);
  points.sortParticles = true;
  scene.add(points);

  renderer = new THREE.WebGLRenderer({ antialias: true });
  renderer.setSize(winWidth, winHeight);
  renderer.setClearColor('#222', 1);
  document.getElementById('canvas').appendChild(renderer.domElement);
}

function animate(){
  requestAnimationFrame(animate);
  
  scene.rotation.y += xSpeed;

 
  var i = particleCount;
  while(i--){
    var particle = particles.vertices[i];

   
    if(particle.y > 1000){
      particle.y = -1000;
      particle.velocity.y = Math.random();
    }
    particle.velocity.y += Math.random() * ySpeed;

    particle.add(particle.velocity);
  }
  points.geometry.verticesNeedUpdate = true;

  render();
}

function render(){
  camera.lookAt(scene.position);
  renderer.render(scene, camera);
}

function onWindowResize(){
  camera.aspect = window.innerWidth / window.innerHeight;
  camera.updateProjectionMatrix();
  renderer.setSize( window.innerWidth, window.innerHeight );
}


console.log