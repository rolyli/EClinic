const axios = require('axios').default;
const instance = axios.create({baseURL: 'http://localhost:5000'})

export default instance