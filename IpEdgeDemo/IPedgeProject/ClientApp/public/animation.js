//Movement Animation to happen
//Moving Animation Event
document.querySelector(".animation_container").addEventListener("mousemove", (e)=>{
  let xAxis = (window.innerWidth/2 - e.pageX)/25;
  let yAxis = (window.innerHeight/2 - e.pageY)/25;
  document.querySelector(".card").style.transform = `rotateY(${xAxis}deg) rotateX(${yAxis}deg)`;
});

//Animate In
document.querySelector(".animation_container").addEventListener("mouseenter", e=>{
  document.querySelector(".card").style.transition = 'none';
  //Popout
  document.querySelector('.title').style.transform = 'translateZ(150px)';
  document.querySelector('.sneaker img').style.transform = 'translateZ(200px) rotateZ(-45deg)';
  document.querySelector('.info h3').style.transform = 'translateZ(125px)';
  document.querySelector('.sizes').style.transform = 'translateZ(100px)';
  document.querySelector('.purchase button').style.transform = 'translateZ(75px)';
});

//Animate Out
document.querySelector(".animation_container").addEventListener("mouseleave", e=>{
  document.querySelector(".card").style.transition = 'all 0.5s ease';
  document.querySelector(".card").style.transform = `rotateY(0deg) rotateX(0deg)`;
  document.querySelector('.title').style.transform = 'translateZ(0px)';
  document.querySelector('.sneaker img').style.transform = 'translateZ(0px) rotateZ(0deg)';
  document.querySelector('.info h3').style.transform = 'translateZ(0px)';
  document.querySelector('.sizes').style.transform = 'translateZ(0px)';
  document.querySelector('.purchase button').style.transform = 'translateZ(0px)';
});