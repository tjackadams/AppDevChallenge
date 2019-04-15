import React, { useState, useEffect } from "react";

import { AlertMap } from "../components/AlertMap";
import axios from "axios";

const baseUrl = process.env.REACT_APP_APIURL;
const subscriptionKey = process.env.REACT_APP_AZUREMAPKEY;

const AlarmsContainer = () => {
  const [data, setData] = useState([]);
  useEffect(() => {
    const fetchData = async () => {
      const result = await axios.get(`${baseUrl}api/Alarms`);
      setData(result.data);
    };

    fetchData();

    const poll = setInterval(() => {
      fetchData();
    }, 5000);

    return () => {
      clearInterval(poll);
    };
  }, []);

  if (data.length === 0) {
    return null;
  }

  return <AlertMap data={data} subscriptionKey={subscriptionKey} />;
};

export default AlarmsContainer;
