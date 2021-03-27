import * as React from 'react';
//import axios from 'axios';
import ApiBase from '../API/apiBase';

interface HitResult {
  resultOrder:number;
  resultPlace:string;
};

interface HitResultView {
  searchEngineName:string;
  resultList:HitResult[];
};

export default function Search() {
  const [keyword, setKeyword] = React.useState<string>();
  const [url, setUrl] = React.useState<string>("www.infotrack.com");
  const [hitResult, setHitResult] = React.useState<any>([]);

  function GetResult() {

    //For senior level, I use own api base function to create post ajax request.
    ApiBase.ajax('post','https://localhost:5001/Search', {SearchUrl:url,SearchKeyword:keyword}).then(
      function(response)
      {
        setHitResult(response);
      }
    );
    
    // For mid level test, I use the axios lib to create the post request.
    // axios.post("/Search", {SearchUrl:url,SearchKeyword:keyword}).then(
    //   function(response)
    //   {
    //     setHitResult(response.data);
    //   }
    // );
  }

  function KeywordOnChange(e:React.ChangeEvent<HTMLInputElement>){
    setKeyword(e.target.value);
  }

  function UrlOnChange(e:React.ChangeEvent<HTMLInputElement>){
    setUrl(e.target.value);
  }

  function RenderResult(){
    if(hitResult.length > 0)
    {
      return(
        <>
        {
          hitResult.map((result:HitResultView, index:number)=>
            {
              return (
                <div key={index}>
                  <h3>Searched from <span className="badge bg-secondary">{result.searchEngineName}</span> for first <span className="badge bg-secondary">{result.resultList.length}</span> results. (Not contain ads results)</h3>
                  {
                    result.resultList.map((item: HitResult, index: number) => {
                      if (item.resultPlace == null) return null;
                      return (
                        <h4 key={index}>The url appears in the No.{item.resultOrder} result. And appears at result's {item.resultPlace} block.</h4>
                      )
                    })
                  }
                </div>
              )
            })
        }
        </>
      )
    }
    else{
      return (<h3>No hit results.</h3>)
    }
  }

  return (
    <div>
      <h1>InfoTrack Search</h1>
      <br></br>
      <p>Please input the keyword you want search engines to search. (default: online title search)</p>
      <input className="form-control" defaultValue="online title search" value={keyword} onChange={KeywordOnChange} />
      <br></br>
      <p>Please input the Url (or other keyword) you want find from the search results.(default: www.infotrack.com)</p>
      <input className="form-control" value={url} onChange={UrlOnChange} />
      <br></br>
      <button className="btn btn-primary" onClick={GetResult}>Get the results</button>
      <br></br>
      <div>
        <h3>Hit Results:</h3>
        {RenderResult()}
      </div>
    </div>
  )
}