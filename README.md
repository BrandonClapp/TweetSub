# TwitterStream
Bot for subscribing to live Twitter tweets and dispatching them to other medium.

### Credentials

Credentials can be configured in `credentials.dev.json` (careful, as this is part of source control) or `credentials.json` (not committed to source control).

Credentials for running the bot can be obtained at https://apps.twitter.com/

### Subscription Topics

`tweet-filters.json` contains an array of strings. Each string represents a topic that you would like to subscribe to.
Multiple words in the same topic are `AND`'d together, so long as all words occur in the tweet. 
Each additional topic added is `OR`'d together with the previous topics.

i.e.

**tweet-filters.json**
```json
[
    "hiring .NET developer",
    "baseball"
]
```

Would match:

```
We are hiring an awesome .NET solutions developer to join our team.
```

as well as
```
I love baseball cards!
```
