# hubhacks2
City of Boston Hubhacks2 submission

Submission for http://hubhacks2.challengepost.com/

The goal of this project is to explore alternative and engaging technologies for visualizing  public sentiment associated with City services and events as they are spatially distributed in the city. Our approach was very ambitious within the scope of the hackathon, and our final product represents more of a starting point than a final, polished product. Our vision was to build a basic proof of concept showing how 3D gaming technology could be used to build the foundation for an interactive 3D data-rich world that can be interacted with and explored, as well as also supplement this information with new tools to collect public sentiment data. 

The concept of  this system is to engage new constituent groups in a fun, interactive way, while providing City staff with unique feedback on how they are providing services.  The idea is to build a framework both visualization and **COLLECTING** public sentiment from multiple city data streams that interact with each other, hopefully engaging younger gaming culture in city functions in a fun and instructive way.

There are several components of the system. The first is the main visualization tool. This tool provides a 3D view of the city that shows public sentiment represented by “happiness clouds” These happiness clouds can be displayed in either historical mode, or real time mode, showing data live as it is streaming from the City of Boston servers or from data collected from the “Boston Voice” mobile App. The tools allows the user to navigate around the 3D recreation of the city from a floating camera and see how public sentiment is distributed around the City.

The visualization tool streams the following data from city servers and represents it on the 3D world as “happiness” clouds:

- Waze Alert Data
- Mayor’s 24 Hour Hotline data
- 911 Police
- 911 Fire

These data sets are used as generalized indicators of potential public sentiment, but can also be used to inform solicitation for how people feel about those issues that are near them. 

The second component of the system is an new app that we created for this project that allows a user to publish their feelings (via smartphone) about several core City of Boston services: Transportation, Sanitation, Safety, and Overall satisfaction or happiness with the city.  Additionally, and importantly, the app also allows the City to push out surveys to it’s constituents who have the app. The surveys can be either location based, or general. Location based surveys are triggered when the user is near a City event  (triggered by GPS coordinates) and asks targeted question of the user relating to their feelings about that event or location. These results are stored anonymously on the server (although the capability to support profiles is possible). This collected survey data can be shown in real time in the visualization application.

Obviously the real value of this type of tool would only be realized once a large number of constituents utilized the tool, but having access to an app that lets them see their sentiment registered along with others in a 3D landscape is a compelling and dynamic way to engage new, younger audience in City *service*.

