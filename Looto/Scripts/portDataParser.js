const axios = require('axios');
const cheerio = require('cheerio');

const wikiPortsArticleUrl = 'https://en.wikipedia.org/wiki/List_of_TCP_and_UDP_port_numbers';

try {
    const wikiHtml = await axios.get(wikiPortsArticleUrl);
    console.log(wikiHtml);
} catch (err) {
    console.error('Wiki article about ports usege parsed with error.');
    console.error(`Error info: ${err}`);
}
