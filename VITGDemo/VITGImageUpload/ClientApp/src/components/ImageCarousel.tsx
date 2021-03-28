import * as React from 'react';
import axios from 'axios';
import { Carousel, CarouselItem } from 'react-bootstrap';

export default function ImageCarousel()
{
  const [imageList, setImageList] = React.useState<any>([]);

  React.useEffect(()=>{
    getImageList();
  },[])

  async function getImageList()
  {
    setImageList (await axios.get("/api/uploadimage"));
  }

  function RenderImageList(){
    console.log(imageList);
    if(imageList.data === undefined) return;
    console.log(imageList.data);
    return (
      <Carousel>
        {
          imageList.data.map((item:any, index: number) => {
            const imgUrl = 'data:image;base64,' + item.rawData;
            console.log(imgUrl);
            return (
                <CarouselItem key={index}>
                  <img src={imgUrl} className="d-block w-100" alt={item.metaData}/>
                  <p>{item.metaData}</p>
                </CarouselItem>
            )
          })
        }
      </Carousel>
    )
  }

  return (
    <div id="carouselExampleSlidesOnly" className="carousel slide" data-bs-ride="carousel">
      <div className="carousel-inner">
        {RenderImageList()}
        <button  className="btn btn-primary" onClick={getImageList}>Update</button>
      </div>
    </div>
  )
}