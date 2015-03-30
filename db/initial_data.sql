/*
AArace 20150316 - Initial sample data population

truncate table taken
truncate table answer
delete from question
delete from questiontype
delete from locationsurvey
delete from location
delete from survey
delete from surveytype
delete from activation

*/

/*
	SURVEY TYPE
*/
if not exists(select null from surveytype)
begin
	set identity_insert surveytype on
	
	insert into surveytype (id, description)
	values (1, 'Global Survey, no location')
	
	insert into surveytype (id, description)
	values (2, 'Anytime Survey, location-based')

	insert into surveytype (id, description)
	values (3, 'Location Survey, location-based')

	insert into surveytype (id, description)
	values (4, 'Event Survey, event-based')

	set identity_insert surveytype off
end


/*
	QUESTION TYPE
*/
if not exists(select null from questiontype)
begin
	set identity_insert questiontype on
	
	insert into QuestionType (id, description)
	values (1, 'Yes/No Question')

	insert into QuestionType (id, description)
	values (2, 'Scale of 1-10 Question')

	set identity_insert questiontype off
end

declare @id uniqueidentifier
declare @lId uniqueidentifier

/*
	SURVEY
*/
if not exists(select null from survey)
begin

	insert into survey (name, surveytypeid, description)
	values ('Boston Happiness Survey', 1, 'The simplest of surveys about your happiness in the city.')
		select @id = id from survey where name = 'Boston Happiness Survey'

		insert into question (SurveyId, QuestionTypeId, questiontext)
		values (@id, 1, 'Have your experiences in Boston been happy?')

	insert into survey (name, surveytypeid, description)
	values ('Boston Cleanliness Survey', 2, 'Help Boston streets shine. Tell us how clean your current location is, and help our Public Workds department keep our city streets, sidewalks, and public spaces presentable.')
		select @id = id from survey where name = 'Boston Cleanliness Survey'

		insert into question (SurveyId, QuestionTypeId, questiontext)
		values (@id, 1, 'Is there any visible trash or litter at your current location?')

		insert into question (SurveyId, QuestionTypeId, questiontext)
		values (@id, 1, 'Is there any visible graffiti at your current location?')
		
		insert into question (SurveyId, QuestionTypeId, questiontext, labellow, LabelHigh, LabelHighGreen)
		values (@id, 2, 'Overall, how clean is your current location?', 'Very Dirty', 'Sparkling Clean', 1)


	insert into survey (name, surveytypeid, description)
	values ('Safety Survey', 2, 'Help every corner of Boston be safe for all of its residents and visitors. Take this quick survey to let us know how safe you feel your current location is.')
		select @id = id from survey where name = 'Safety Survey'

		insert into question (SurveyId, QuestionTypeId, questiontext, LabelLow, LabelHigh, LabelHighGreen)
		values (@id, 2, 'Overall, how safe does your current location feel?', 'very scary', 'very safe', 1)

		insert into question (SurveyId, QuestionTypeId, questiontext)
		values (@id, 1, 'Are there any visible police or fire call boxes?')

		insert into question (SurveyId, QuestionTypeId, questiontext)
		values (@id, 1, 'Are any police or public officials visible?')

		insert into question (SurveyId, QuestionTypeId, questiontext)
		values (@id, 1, 'Do you see any immediate danger to public safety?')


	insert into survey (name, surveytypeid, description)
	values ('Transportation Survey', 2, 'How is Boston''s Transportation system faring? Take this survey to tell us what needs to be improved in the transportation system around your current location.')
		select @id = id from survey where name = 'Transportation Survey'

		insert into question (SurveyId, QuestionTypeId, questiontext, LabelLow, LabelHigh)
		values (@id, 2, 'How congested is the traffic at your current location?', 'free flowing traffic', 'gridlock')

		insert into question (SurveyId, QuestionTypeId, questiontext)
		values (@id, 1, 'Is there public transportation (MBTA subway, bus, etc) available?')

		insert into question (SurveyId, QuestionTypeId, questiontext)
		values (@id, 1, 'Is other transportation, such as cabs, easily visible?')

	insert into survey (name, surveytypeid, description)
	values ('Neighborhood Happiness', 2, '')
		select @id = id from survey where name = 'Neighborhood Happiness'

		insert into question (SurveyId, QuestionTypeId, questiontext, LabelLow, LabelHigh, LabelHighGreen)
		values (@id, 2, 'How happy does your current your current location make you feel?', 'unhappy', 'super happy', 1)

end



if not exists(select null from location)
begin
	insert into Location (name, toDate, lat, lng, radius)
	values ('HubHacks2', '4/6/2015', 42.352293, -71.045229, 1000)
	
	select @lId = id from Location where name = 'HubHacks2'
	insert into survey (name, surveytypeid, description)
	values ('HubHacks2 Survey', 3, 'Tell us how awesome has this HubHacks has been!')
		select @id = id from survey where name = 'HubHacks2 Survey'

		insert into question (SurveyId, QuestionTypeId, questiontext, LabelLow, LabelHigh, LabelHighGreen)
		values (@id, 2, 'How Awesome?', 'mildly awesome', 'VERY AWESOME!', 1)

		insert into locationsurvey(locationid, surveyid) values (@lid, @id)

	insert into Location (name, toDate, lat, lng, radius)
	values ('BSC', '4/1/2015', 42.339411, -71.036545, 1000)

	select @lId = id from Location where name = 'BSC'
	insert into survey (name, surveytypeid, description)
	values ('BSC Survey', 3, 'Tell us how awesome BSC is!')
		select @id = id from survey where name = 'BSC Survey'

		insert into question (SurveyId, QuestionTypeId, questiontext, LabelLow, LabelHigh, LabelHighGreen)
		values (@id, 2, 'How Awesome?', 'mildly awesome', 'VERY AWESOME!', 1)

		insert into question (SurveyId, QuestionTypeId, questiontext)
		values (@id, 1, 'Do you work there?')

		insert into locationsurvey(locationid, surveyid) values (@lid, @id)


	insert into Location (name, toDate, lat, lng, radius)
	values ('von Wahlde Homestead', '4/6/2015', 42.418967, -71.315149, 1000)
	
	select @lId = id from Location where name = 'von Wahlde Homestead'
	insert into survey (name, surveytypeid, description)
	values ('von Wahlde Survey', 3, 'Tell us how cool anyone with a von in their last name is.')
		select @id = id from survey where name = 'von Wahlde Survey'

		insert into question (SurveyId, QuestionTypeId, questiontext, LabelLow, LabelHigh, LabelHighGreen)
		values (@id, 2, 'How cool is Matt?', 'mildly cool', 'amazingly cool', 1)

		insert into question (SurveyId, QuestionTypeId, questiontext)
		values (@id, 1, 'Did Matt make you say that?')

		insert into locationsurvey(locationid, surveyid) values (@lid, @id)

end