/*
AArace 20150316 - Initial data population

select * from surveytype

delete from question
delete from questiontype
delete from survey
delete from surveytype

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


/*
	SURVEY
*/
if not exists(select null from survey)
begin
	declare @id uniqueidentifier

	insert into survey (name, surveytypeid, description)
	values ('Boston Cleanliness Survey', 2, 'Help Boston streets shine. Tell us how clean your current location is, and help our Public Workds department keep our city streets, sidewalks, and public spaces presentable.')
		select @id = id from survey where name = 'Boston Cleanliness Survey'

		insert into question (SurveyId, QuestionTypeId, questiontext)
		values (@id, 1, 'Is there any visible trash or litter at your current location?')

		insert into question (SurveyId, QuestionTypeId, questiontext)
		values (@id, 1, 'Is there any visible graffiti at your current location?')
		
		insert into question (SurveyId, QuestionTypeId, questiontext)
		values (@id, 2, 'Overall, how clean is your current location? (1=very dirty, 10=sparkling clean)')


	insert into survey (name, surveytypeid, description)
	values ('Safety Survey', 2, 'Help every corner of Boston be safe for all of its residents and visitors. Take this quick survey to let us know how safe you feel your current location is.')
		select @id = id from survey where name = 'Safety Survey'

		insert into question (SurveyId, QuestionTypeId, questiontext)
		values (@id, 2, 'Overall, how safe does your current location feel? (1=feels dangerous and scary, 10=feels very safe)')

		insert into question (SurveyId, QuestionTypeId, questiontext)
		values (@id, 1, 'Are there any visible police or fire call boxes?')

		insert into question (SurveyId, QuestionTypeId, questiontext)
		values (@id, 1, 'Are any police or public officials visible?')

		insert into question (SurveyId, QuestionTypeId, questiontext)
		values (@id, 1, 'Do you see any immediate danger to public safety?')


	insert into survey (name, surveytypeid, description)
	values ('Transportation Survey', 2, 'How is Boston''s Transportation system faring? Take this survey to tell us what needs to be improved in the transportation system around your current location.')
		select @id = id from survey where name = 'Transportation Survey'

		insert into question (SurveyId, QuestionTypeId, questiontext)
		values (@id, 2, 'How congested is the traffic at your current location? (1=free flowing traffic, 10=gridlock)')

		insert into question (SurveyId, QuestionTypeId, questiontext)
		values (@id, 1, 'Is there public transportation (MBTA subway, bus, etc) available?')

		insert into question (SurveyId, QuestionTypeId, questiontext)
		values (@id, 1, 'Is other transportation, such as cabs, easily visible?')
end
