CREATE VIEW [dbo].[Program_Workouts]
AS

SELECT [Programs].[Id] AS [ProgramId], [Programs].[Name] AS [ProgramName], [Workouts].[Id] AS [WorkoutId], 
[Workouts].[Name] AS [WorkoutName]

FROM [Programs]

INNER JOIN [WorkoutPrograms] ON [Programs].[Id] = [WorkoutPrograms].[fk_ProgramId]
INNER JOIN [Workouts] ON [WorkoutPrograms].[fk_WorkoutId] = [Workouts].[Id]
