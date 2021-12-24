CREATE VIEW [dbo].[Program_Workouts]
AS

SELECT [WorkoutPrograms].[Id] AS [Id], [Programs].[Id] AS [ProgramId], [Programs].[Name] AS [ProgramName], [Workouts].[WorkoutId], 
[Workouts].[WorkoutName], [Workouts].[WorkoutInfo], [WorkoutPrograms].[Id] AS [WorkoutProgramId], [WorkoutPrograms].[WorkoutProgramOrder]

FROM [Programs]

INNER JOIN [WorkoutPrograms] ON [Programs].[Id] = [WorkoutPrograms].[fk_ProgramId]
INNER JOIN [Workouts] ON [WorkoutPrograms].[fk_WorkoutId] = [Workouts].[WorkoutId]


