# Napier Bank Message Filtering Service

## Scenario
Napier Bank is a medium-sized local bank with many thousands of users. The bank operates from one headquarters and a number of branches. It is required to develop a service, namely Napier Bank Messaging (NBM), which will validate, sanitise and categorise incoming messages to Napier Bank in the form of SMS text messages, emails and Tweets.

## Message Types
The system must deal with three types of message
All messages are strings of ASCII characters that have a Message Header comprising a Message ID (Message-type “S”,”E” or “T” followed by 9 numeric characters, e.g. “E1234567701”) followed by the Body of the message.

**SMS Messages**
SMS message bodies comprise Sender in the form of an international telephone phone number followed by the Message Text which is a maximum of 140 characters long. The Message Text message is simple text but may contain embedded “textspeak abbreviations”. Textspeak abbreviations that may be embedded are in textwords.csv file.

**Email Messages**
Email message bodies comprise Sender in the form of a standard email address John Smith john.smith@example.org followed by a 20 character Subject followed by the Message Text which is a maximum of 1028 characters long. The Message Text message is simple text but may contain embedded hyperlinks in the form of standard URLs e.g. http:\\www.anywhere.com. 

**Tweets**
Tweet bodies comprise Sender in the form of a Twitter ID: “@” followed by a maximum of 15 characters (e.g. @JohnSmith) and the Tweet text which is a maximum of 140 characters long. In addition to ordinary text the Tweet text may contain any of the following:
 - textspeak abbreviations (as in SMS above)
 - hashtags - strings of characters preceded by a ‘#’ sign that are used to group posts by topic. (such as #BBCClick, #1Donice).
 - Twitter IDs as above

## System Development
This is a prototype WPF application developed in C#/.NET that enable the inputting of messages in any of the forms stated above. The system detects the message type and output each message in JSON format in a file. The system is also able to read XML files to process messages and display on-screen.

## Message Processing
Messages are processed as follows:

**SMS Messages:** Textspeak abbreviations must be expanded to their full form enclosed in “<>”, e.g. “Saw your message ROFL can’t wait to see you” becomes “Saw your message ROFL can’t wait to see you”

**Email Messages:** 
Email messages are of two types: Standard email messages and Significant Incident Reports that comprise text reports from bank branch managers concerning incidents of significance that happened during the working day, such as robberies, significant cash shortages, violent incidents. Both types may contain embedded URLs Standard email messages will contain text. Any URLs contained in messages will be removed and written to a quarantine list and replaced by “” in the body of the message.

Significant Incident Reports will have the Subject in the form “SIR dd/mm/yy” and will comprise a message body as above. The message body will begin with the following standard texts on the first two lines: 
Sort Code: 99-99-991 
Nature of Incident: which will be one of the following
| Significant Incident Reports              |
|---------------------|
| Theft         |
| Staff Attack        |
| ATM Theft           |
| Raid                |
| Customer Attack     |
| Staff Abuse         |
| Bomb Threat         |
| Terrorism           |
| Suspicious Incident |
| Intelligence        |
| Cash Loss           |

Sort Code and Nature of Incident will be written to a SIR list. Any URLs contained in messages will be removed and written to a quarantine list and replaced by “” in the body of the message.

**Tweets:** Textspeak abbreviations will be expanded (as in SMS messages above). Hashtags will be added to a hashtag list that will count the number of uses of each to produce a trending list. “Mentions”, i.e. embedded Twitter IDs will be added to a mentions list.
