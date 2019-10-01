import random

def RPSSL():
    print("Hello, Player!")
    print("You need to input one of 5 variant")
    print("Stone, Scissors, Paper, Spock or Lizard")
    print("If you want to exit the game, please input - EXIT")
    print("Good Luck and Have Fun")

    variants_player = {
        0: "Stone",
        1: "Scissors",
        2: "Paper",
        3: "Spock",
        4: "Lizard",
    }

    while True:
        playerChoice = input("Please input your variant.\n")
        if playerChoice == "EXIT":
            break
        ai_choice = random.randint(0, 4)

        print("Choice of player: " + playerChoice + "\n")
        print("Choice of AI: " + variants_player[ai_choice] + "\n")

        if playerChoice == variants_player[ai_choice]:
            print("Equal variants. Please try again.\n")
        elif playerChoice == "Stone" and variants_player[ai_choice] in ["Spock", "Paper"]:
            print("You lose. Try again\n")
        elif playerChoice == "Paper" and variants_player[ai_choice] in ["Scissors", "Lizard"]:
            print("You lose. Try again\n")
        elif playerChoice == "Scissors" and variants_player[ai_choice] in ["Stone", "Spock"]:
            print("You lose. Try again\n")
        elif playerChoice == "Spock" and variants_player[ai_choice] in ["Paper", "Lizard"]:
            print("You lose. Try again\n")
        elif playerChoice == "Lizard" and variants_player[ai_choice] in ["Scissors", "Stone"]:
            print("You lose. Try again\n")
        else:
            print("YOU WIN!!!\n")


if __name__ == "__main__":
    RPSSL()
    print("Goodbye!")
